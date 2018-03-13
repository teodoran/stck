module Stck.Tests

open System.IO
open Stck
open Xunit
open FsUnit.Xunit

let stdlibContext =
        File.ReadAllText(stdlib)
        |> sprintf "```%s```"
        |> (fun lib -> eval lib emptyContext)

let allTestsPassed c =
    let s = stringify c
    match s.Replace("true", "").Trim() with
    | "" -> true
    | _ ->
        printfn "STCK test runner information:"
        printfn "%s" s
        false

[<Fact>]
let ``core operations tests should all pass`` () =
    let coreOperationsTests =
        File.ReadAllText("core-operations-tests.md")
        |> sprintf "```%s```"
        |> (fun lib -> eval lib stdlibContext)
    
    allTestsPassed coreOperationsTests |> should be True

[<Fact>]
let ``stdlib tests should all pass`` () =
    let coreOperationsTests =
        File.ReadAllText("stdlib-tests.md")
        |> sprintf "```%s```"
        |> (fun lib -> eval lib stdlibContext)
    
    allTestsPassed coreOperationsTests |> should be True

[<Fact>]
let ``project euler tests should all pass`` () =
    let coreOperationsTests =
        File.ReadAllText("project-euler-tests.md")
        |> sprintf "```%s```"
        |> (fun lib -> eval lib stdlibContext)
    
    allTestsPassed coreOperationsTests |> should be True