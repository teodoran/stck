module Stck.Console

open System
open System.IO
open System.Reflection
open Stck

let cprintfn c fmt =
    let clr s =
        let old = Console.ForegroundColor
        try
          Console.ForegroundColor <- c;
          System.Console.Write (s + "\n")
        finally
          Console.ForegroundColor <- old

    Printf.kprintf clr fmt

let rec color = function
    | Empty -> ConsoleColor.Yellow
    | Stack (Exception _, _) -> ConsoleColor.Red
    | Stack (_, t) -> color t

let prompt c =
    printf "$> "
    c

let print strfy c =
    let _, s = c
    cprintfn (color s) "[%s]" (strfy c)
    prompt c

let hprint strfy c =
    let Heap hm, _ = c

    hm
    |> Map.toSeq
    |> Seq.iter (fun d ->
        let w, s = d
        cprintfn ConsoleColor.Cyan "%s ->" w
        cprintfn ConsoleColor.Yellow "    [%s]" (strfy (Heap hm, (reverse s))))

    prompt c

let stdlibFile =
    Path.Combine(
        Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),
        stdlib)

let load f c =
    match File.Exists(f) with
    | true -> eval (sprintf "```%s```" (File.ReadAllText(f))) c
    | false ->
        cprintfn ConsoleColor.Magenta "The file %s does not exist" f
        c

let rec read () = Console.ReadLine().Trim()

let quit () =
    cprintfn ConsoleColor.Green "%s" "Bye!"
    exit 0
let rec loop strfy (c : Context) : Context =
    match read () with
    | "quit" -> quit ()
    | "church" -> c |> print stringifyc |> loop stringifyc
    | "unchurch" -> c |> print stringify |> loop stringify
    | "hprint" ->
        c |> hprint strfy |> loop strfy
    | s when s.StartsWith("load ") ->
        c |> load (s.Replace("load ", "")) |> prompt |> loop strfy
    | exps ->
        c |> eval exps |> print strfy |> loop strfy

[<EntryPoint>]
let main args =
    match Array.length args > 0 with
    | true -> load args.[0] emptyContext |> ignore
    | false ->
        printfn ""
        printfn "Welcome to STCK 2.0, a stack-based programming language"
        printfn ""
        emptyContext
        |> load stdlibFile
        |> prompt
        |> loop stringify
        |> ignore
    0
