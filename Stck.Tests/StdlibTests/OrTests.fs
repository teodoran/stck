module StdlibTests.OrTests

open System.IO
open Xunit
open FsUnit.Xunit
open Stck

let emptyContext = (Heap Map.empty, Empty)
let stdlibContext =
    File.ReadAllText(stdlib)
    |> sprintf "```%s```"
    |> (fun lib -> eval lib emptyContext)

[<Fact>]
let ``true true or should be true #1`` () =
    let expectedStack = Stack (Operation "foo", Empty)
    let _, actualStack = (eval "true true or [foo] [bar] ?" stdlibContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``true true or should be true #2`` () =
    let expectedStack = Stack (Operation "foo", Empty)
    let _, actualStack = (eval "foo bar true true or app" stdlibContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``false true or should be true #1`` () =
    let expectedStack = Stack (Operation "foo", Empty)
    let _, actualStack = (eval "false true or [foo] [bar] ?" stdlibContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``false true or should be true #2`` () =
    let expectedStack = Stack (Operation "foo", Empty)
    let _, actualStack = (eval "foo bar false true or app" stdlibContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``true false or should be true #1`` () =
    let expectedStack = Stack (Operation "foo", Empty)
    let _, actualStack = (eval "true false or [foo] [bar] ?" stdlibContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``true false or should be true #2`` () =
    let expectedStack = Stack (Operation "foo", Empty)
    let _, actualStack = (eval "foo bar true false or app" stdlibContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``false false or should be false #1`` () =
    let expectedStack = Stack (Operation "bar", Empty)
    let _, actualStack = (eval "false false or [foo] [bar] ?" stdlibContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``false false or should be false #2`` () =
    let expectedStack = Stack (Operation "bar", Empty)
    let _, actualStack = (eval "foo bar false false or app" stdlibContext)
    
    actualStack |> should equal expectedStack