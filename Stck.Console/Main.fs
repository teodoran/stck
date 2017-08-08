module Stck.Console

open System
open System.IO
open Stck

let emptyContext : Context = (Heap Map.empty, Empty)

let cprintfn c fmt =
    let clr s =
        let old = System.Console.ForegroundColor
        try
          System.Console.ForegroundColor <- c;
          System.Console.Write (s + "\n")
        finally
          System.Console.ForegroundColor <- old

    Printf.kprintf clr fmt

let rec strs = function
    | Empty -> ""
    | Stack (e, Empty) -> stre e
    | Stack (e, r) -> sprintf "%s %s" (strs r) (stre e)
and stre = function
    | Operation w -> w
    | Quotation q -> sprintf "[%s]" (strs q)
    | Exception e ->
        match e with
        | StackUnderflow -> "Exception: StackUnderflow"
        | MissingQuotation -> "Exception: MissingQuotation"
        | Failure s -> sprintf "Exception: %s" s

let rec color = function
    | Empty -> ConsoleColor.Yellow
    | Stack (Exception _, _) -> ConsoleColor.Red
    | Stack (_, t) -> color t

let prompt c =
    printf "$> "
    c

let print c =
    let _, s = c
    cprintfn (color s) "[%s]" (strs s)
    prompt c

let hprint c =
    let Heap hm, _ = c
    
    hm
    |> Map.toSeq
    |> Seq.iter (fun d ->
        let w, s = d
        cprintfn ConsoleColor.Cyan "%s ->" w
        cprintfn ConsoleColor.Yellow "    [%s]" (strs s))

    prompt c

let load f c =
    match File.Exists(f) with
    | true ->
        c
        |> exec (File.ReadAllText(f))
        |> print
    | false ->
        cprintfn ConsoleColor.Magenta "The file %s does not exist" f
        prompt c

let rec loop (c : Context) : Context =
    let exps : Program = Console.ReadLine().Trim()
    match exps with
    | "#quit" ->
        cprintfn ConsoleColor.Green "%s" "Bye!"
        exit 0
    | "#hprint" ->
        c |> hprint |> loop
    | s when s.StartsWith("#load ") ->
        c |> load (s.Replace("#load ", "")) |> loop
    | _ ->
        c |> exec exps |> print |> loop

[<EntryPoint>]
let main args =
    match Array.length args > 0 with
    | true -> load args.[0] emptyContext |> ignore
    | false ->
        printfn ""
        printfn "Welcome to STCK 2.0, a stack-based programming language"
        printfn ""
        printf "$> "
        loop emptyContext |> ignore
    0
