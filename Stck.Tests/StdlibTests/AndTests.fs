module StdlibTests.AndTests

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
let ``true true and should be true #1`` () =
    let expectedStack = Stack (Operation "foo", Empty)
    let _, actualStack = (eval "true true and [foo] [bar] ?" stdlibContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``true true and should be true #2`` () =
    let expectedStack = Stack (Operation "foo", Empty)
    let _, actualStack = (eval "foo bar true true and app" stdlibContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``false true and should be false #1`` () =
    let expectedStack = Stack (Operation "bar", Empty)
    let _, actualStack = (eval "false true and [foo] [bar] ?" stdlibContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``false true and should be false #2`` () =
    let expectedStack = Stack (Operation "bar", Empty)
    let _, actualStack = (eval "foo bar false true and app" stdlibContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``true false and should be false #1`` () =
    let expectedStack = Stack (Operation "bar", Empty)
    let _, actualStack = (eval "true false and [foo] [bar] ?" stdlibContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``true false and should be false #2`` () =
    let expectedStack = Stack (Operation "bar", Empty)
    let _, actualStack = (eval "foo bar true false and app" stdlibContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``false false and should be false #1`` () =
    let expectedStack = Stack (Operation "bar", Empty)
    let _, actualStack = (eval "false false and [foo] [bar] ?" stdlibContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``false false and should be false #2`` () =
    let expectedStack = Stack (Operation "bar", Empty)
    let _, actualStack = (eval "foo bar false false and app" stdlibContext)
    
    actualStack |> should equal expectedStack