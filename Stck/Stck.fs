module Stck

type Word = string

type Error = string

type StackElement =
    | Operation of Word
    | Exception of Error
    | Quotation of Stack
and Stack =
    | Empty
    | Stack of (StackElement * Stack)

type Heap = Heap of Map<Word, Stack>

type Context = (Heap * Stack)

let emptyContext : Context = (Heap Map.empty, Empty)

let stdlib = "stdlib.md"

let underflow = Exception "STACK_UNDERFLOW"

let push e s = Stack (e, s)

let drop = function
    | Stack (_, r) -> r
    | Empty -> push underflow Empty

let dup = function
    | Stack (e, t) -> Stack (e, Stack (e, t))
    | Empty -> push underflow Empty

let swap = function
    | Stack (a, Stack (b, t)) -> Stack (b, Stack (a, t))
    | s -> push underflow s

let ontop = function
    | Stack (e, Stack (Quotation q, r)) -> Stack (Quotation (push e q), r)
    | s -> push underflow s

let rec tail e s =
    match s with
    | Empty -> Stack (e, Empty)
    | Stack (h, t) -> Stack (h, tail e t)

let ontail = function
    | Stack (e, Stack (Quotation q, r)) -> Stack (Quotation (tail e q), r)
    | s -> push underflow s

let rec stail e s =
    match s with
    | Empty -> e
    | Stack (h, t) -> Stack (h, stail e t)

let concat = function
    | Stack (Quotation q, Stack (Quotation q', r)) -> Stack (Quotation (stail q q'), r)
    | s -> push underflow s

let chop = function
    | Stack (Quotation Empty, r) -> Stack (Quotation Empty, Stack (Quotation Empty, r))
    | Stack (Quotation (Stack (a, t)), r) -> Stack (Quotation (Stack (a, Empty)), Stack (Quotation t, r))
    | s -> push underflow s

let emp = function
    | Empty -> push (Quotation (Stack (Operation "true", Empty))) Empty
    | s -> push (Quotation (Stack (Operation "false", Empty))) s

let throw = function
    | Stack (Operation a, r) -> push (Exception a) r
    | s -> push underflow s

let err = function
    | Stack (Exception _, r) -> push (Quotation (Stack (Operation "true", Empty))) r
    | Stack (_, r) -> push (Quotation (Stack (Operation "false", Empty))) r
    | s -> push underflow s

let define c =
    let Heap h, s = c
    match s with
    | Stack (Operation w, Stack (Quotation q, r)) -> (Heap (Map.add w q h), r)
    | _ -> (Heap h, (push underflow s))

let rec apply s c : Context =
    match s with
    | Empty -> c
    | Stack (e, r) -> apply r (exec e c)
and exec e c =
    let h, s = c
    let Heap hm, _ = c
    match e with
    | Operation "." -> (h, drop s)
    | Operation "dup" -> (h, dup s)
    | Operation "swap" -> (h, swap s)
    | Operation "<<" -> (h, ontop s)
    | Operation ">>" -> (h, ontail s)
    | Operation "||" -> (h, concat s)
    | Operation "|" -> (h, chop s)
    | Operation "emp" -> (h, emp s)
    | Operation "throw" -> (h, throw s)
    | Operation "err" -> (h, err s)
    | Operation "#" -> define c
    | Operation "app" -> app c
    | Operation "eq" -> eq c
    | Operation w when Map.containsKey w hm -> apply (Map.find w hm) c
    | Exception _ -> c
    | symbol -> (h, push symbol s)
and app c =
    let h, s = c
    match s with
    | Stack (Quotation q, r) -> apply q (h, r)
    | _ -> (h, push (Exception "MISSING_QUOTATION") s)
and eq c =
    let h, s = c
    match s with
    | Stack (Quotation q1, Stack (Quotation q2, r)) ->
        let ha, a = apply q1 (h, Empty)
        let hb, b = apply q2 (h, Empty)
        match a = b with
        | true -> (ha, push (Quotation (Stack (Operation ".", Empty))) r)
        | false -> (hb, push (Quotation (Stack (Operation "swap", Stack (Operation ".", Empty)))) r)
    | _ -> (h, push (Exception "MISSING_QUOTATION") s)

let rec skip = function
    | [] | ["```"] -> []
    | "```"::t -> t
    | _::t -> skip t

let lift t =
    let rec recur n l r =
        match r with
        | [] | ["]"] -> (l |> List.rev, [])
        | "]"::t when n = 0 -> (l |> List.rev, t)
        | "["::t -> recur (n + 1) ("["::l) t
        | "]"::t -> recur (n - 1) ("]"::l) t
        | h::t -> recur n (h::l) t
    recur 0 [] t

let rec parse = function
    | [] -> Empty
    | "```"::r -> parse (skip r)
    | "["::lr ->
        let l, r = lift lr
        Stack (Quotation (parse l), parse r)
    | w::r -> Stack (Operation w, parse r)

let replace (a : string) (b : string) (s : string) : string = s.Replace(a, b)

let sugar p =
    p
    |> replace "\n" ""
    |> replace "\n" ""
    |> replace "." " . "
    |> replace "[" " [ "
    |> replace "]" " ] "
    |> replace "```" " ``` "

let lex p =
    p
    |> sugar
    |> (fun s -> s.Split([|' '|]))
    |> Array.toList
    |> List.filter (function
        | "" -> false
        | _ -> true)

let eval (p : string) (c : Context) : Context = (lex >> parse >> apply) p c

let rec numeral = function
    | Empty -> 0
    | Stack (_, r) -> 1 + (numeral r)

let rec isnumeral = function
    | Empty -> true
    | Stack (Operation "I", r) -> isnumeral r
    | _ -> false

let unchurch h (n : Stack) =
    let _, s = app (h, Stack (Quotation n, Stack (Quotation (Stack (Operation "I", Empty)), Empty)))
    (isnumeral s, numeral s)

let churchq (h : Heap) (s : Stack) f : string = sprintf "[%s]" (f (h, s))

let unchurchq (h : Heap) (s : Stack) f : string =
    match unchurch h s with
    | (true, n) -> sprintf "%d" n
    | _ -> sprintf "[%s]" (f (h, s))

let rec stringifyf f = function
    | (_, Empty) -> ""
    | (h, Stack (e, Empty)) -> stre h e f
    | (h, Stack (e, r)) -> sprintf "%s %s" (stringifyf f (h, r)) (stre h e f)
and stre (h : Heap) (se : StackElement) f =
    match se with
    | Operation w -> w
    | Quotation (Stack (Operation ".", Empty)) -> "true"
    | Quotation ((Stack (Operation "swap", Stack (Operation ".", Empty)))) -> "false"
    | Quotation q -> f h q (stringifyf f)
    | Exception e -> sprintf "Exception: %s" e

let rec stringify = stringifyf unchurchq

let rec stringifyc = stringifyf churchq