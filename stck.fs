open System
open System.IO
open System.Text.RegularExpressions

let push e stack =
    e :: stack

let drop stack =
    match stack with
    | tos :: rest -> rest
    | _ ->
        printfn "Cannot drop from the stack"
        stack

let swap stack =
    match stack with
    | a :: b :: rest -> b :: a :: rest
    | _ ->
        printfn "Cannot swap on the stack"
        stack

let dup stack =
    match stack with
    | tos :: rest -> tos :: tos :: rest
    | _ ->
        printfn "Cannot dup on the stack"
        stack

let over stack =
    match stack with
    | a :: b :: rest -> (b :: a :: b :: rest)
    | _ ->
        printfn "Cannot over on the stack"
        stack

let rot stack =
    match stack with
    | a :: b :: c :: rest -> c :: a :: b :: rest
    | _ ->
        printfn "Cannot rot on the stack"
        stack

let isInt string =
    let couldParse, value = Int32.TryParse(string)
    couldParse

let math op stack =
    match stack with
    | a :: b :: rest -> push (op a b) rest
    | _ ->
        printfn "Cannot do math on the stack"
        stack

let add stack = math (fun a b -> b + a) stack

let substract stack = math (fun a b -> b - a) stack

let multiply stack = math (fun a b -> b * a) stack

let divide stack = math (fun a b -> b / a) stack

let modulo stack = math (fun a b -> b % a) stack

let asInt b = if b then 1 else 0

let equal stack = math (fun a b -> asInt(a = b)) stack

let greater stack = math (fun a b -> asInt(a > b)) stack

let less stack = math (fun a b -> asInt(a < b)) stack

let not stack =
    match stack with
    | tos :: rest -> 
        if (tos <> 0) then
            0 :: rest
        else
            1 :: rest
    | _ ->
        printfn "Cannot not on the stack"
        stack

let sprint stack =
    printfn "%A" stack
    stack

let rec hprint heap =
    match heap with
    | [] -> printf ""
    | head :: tail ->
        printfn "# %s %A" (fst head) (snd head)
        hprint tail

let print hs =
    let stack = snd hs
    sprint stack |> ignore
    hs

let exec exp stack =
    match exp with
    | "." -> drop stack
    | "swap" -> swap stack
    | "dup" -> dup stack
    | "over" -> over stack
    | "rot" -> rot stack
    | "+" -> add stack
    | "-" -> substract stack
    | "*" -> multiply stack
    | "/" -> divide stack
    | "%" -> modulo stack
    | "=" -> equal stack
    | ">" -> greater stack
    | "<" -> less stack
    | "not" -> not stack
    | "sprint" -> sprint stack
    | _ ->
        if isInt exp then
            push (Int32.Parse(exp)) stack
        else
            printfn "Does not compute %s" exp
            stack

let define s heap =
    s :: heap

let rec find s heap =
    match heap with
    | [] -> []
    | head :: tail ->
        if fst head = s then
            snd head
        else 
            find s tail

let tokens (s:string) =
    s.Split([|' '|]) |> Array.toList

let rec split delim n col exps =
    match exps with
    | [] -> (col |> List.rev, [])
    | head :: tail ->
        match head with
        | "?" -> split delim (n + 1) (head :: col) tail
        | d when d = delim ->
            match n with
            | 0 -> (col |> List.rev, tail)
            | _ -> split delim (n - 1) (head :: col) tail
        | _ -> split delim n (head :: col) tail
            
let cond tos exps =
    let t = split ":" 0 [] exps
    let f = split ";" 0 [] (snd t)

    match tos <> 0 with
    | true -> (fst t) @ (snd f)
    | false -> (fst f) @ (snd f)

let rec eval exps hs =
    let heap = fst hs
    let stack = snd hs

    match exps with
    | [] -> (heap, stack)
    | head :: tail ->
        match head with
        | "//" -> (heap, stack)
        | "hprint" ->
            hprint heap
            eval tail hs
        | "#" ->
            match tail with
            | [] -> (heap, stack)
            | name :: definition -> (define (name, definition) heap, stack)
        | "?" ->
            match stack with
            | [] -> (heap, stack)
            | tos :: rest -> eval (cond tos tail) (heap, rest)
        | _ ->
            match find head heap with
            | [] -> eval tail (heap, exec head stack)
            | def -> eval (def @ tail) hs


let rec loop hs =
    let exps = tokens (Console.ReadLine())

    hs
    |> eval exps
    |> print
    |> loop

let rec evaluate statements hs =
    match statements with
    | [] -> snd hs
    | head :: tail -> evaluate tail (eval (tokens head) hs)

let lines (s:string) =
    let trimmed = Regex.Replace(s, @"\s+", " ");
    let lines = trimmed.Split([|'!'|]) |> Array.toList

    lines
    |> List.filter (fun line -> line <> " ")
    |> List.map (fun line -> line.Trim())

let rec run file =
    let statements = lines (File.ReadAllText(file))
    System.String.Join(" ", evaluate statements ([], []))

[<EntryPoint>]
let main args =
    match Array.length args > 0 with
    | true ->
        match File.Exists(args.[0]) with
        | false -> printfn "The file %s does not exist" args.[0]
        | true -> printfn "%s" (run args.[0])
    | false ->
        printfn ""
        printfn "Welcome to STCK 1.0, a stack-based programming language"
        printfn ""
        loop ([], [])

    0