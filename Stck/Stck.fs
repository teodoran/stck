module Stck

type Program = string

type Word = string

type Error =
    | StackUnderflow
    | MissingQuotation
    | Failure of string

type StackElement =
    | Operation of Word
    | Exception of Error
    | Quotation of Stack
and Stack =
    | Empty
    | Stack of (StackElement * Stack)

type Heap = Heap of Map<Word, Stack>

type Context = (Heap * Stack)

let push e s = Stack (e, s)

let drop = function
    | Stack (_, r) -> r
    | Empty -> push (Exception StackUnderflow) Empty

let dup = function
    | Stack (e, t) -> Stack (e, Stack (e, t))
    | Empty -> push (Exception StackUnderflow) Empty

let swap = function
    | Stack (a, Stack (b, t)) -> Stack (b, Stack (a, t))
    | s -> push (Exception StackUnderflow) s

let ontop = function
    | Stack (e, Stack (Quotation q, r)) -> Stack (Quotation (push e q), r)
    | s -> push (Exception StackUnderflow) s

let rec tail e s =
    match s with
    | Empty -> Stack (e, Empty)
    | Stack (h, t) -> Stack (h, tail e t)

let ontail = function
    | Stack (e, Stack (Quotation q, r)) -> Stack (Quotation (tail e q), r)
    | s -> push (Exception StackUnderflow) s

let define c =
    let Heap h, s = c
    match s with
    | Stack (Operation w, Stack (Quotation q, r)) -> (Heap (Map.add w q h), r)
    | _ -> (Heap h, (push (Exception StackUnderflow) s))

let rec apply s c : Context =
    match s with
    | Empty -> c
    | Stack (e, r) -> apply r (eval e c)
and eval e c =
    let h, s = c
    let Heap hm, _ = c
    match e with
    | Operation "." -> (h, drop s)
    | Operation "dup" -> (h, dup s)
    | Operation "swap" -> (h, swap s)
    | Operation "<<" -> (h, ontop s)
    | Operation ">>" -> (h, ontail s)
    | Operation "#" -> define c
    | Operation "app" -> app c
    | Operation w when Map.containsKey w hm -> apply (Map.find w hm) c
    | Exception _ -> c
    | symbol -> (h, push symbol s)
and app c =
    let h, s = c
    match s with
    | Stack (Quotation q, r) -> apply q (h, r)
    | _ -> (h, push (Exception MissingQuotation) s)

let rec skip = function
    | [] | ["\""] -> []
    | "\""::t -> t
    | h::t -> skip t

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
    | "\""::r -> parse (skip r)
    | "["::lr ->
        let l, r = lift lr
        Stack (Quotation (parse l), parse r)
    | w::r -> Stack (Operation w, parse r)

let lex (p : Program) =
    p
    |> (fun s -> s.Replace("\n", ""))
    |> (fun s -> s.Replace(".", " . "))
    |> (fun s -> s.Replace("[", " [ "))
    |> (fun s -> s.Replace("]", " ] "))
    |> (fun s -> s.Replace("\"", " \" "))
    |> (fun s -> s.Split([|' '|]))
    |> Array.toList
    |> List.filter (function
        | "" -> false
        | _ -> true)

let exec (p : Program) (c : Context) : Context = apply (parse (lex p)) c
