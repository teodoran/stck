module StdlibTests.BooleanTests

open Xunit
open FsUnit.Xunit
open Stck

let emptyContext = (Heap Map.empty, Empty)
let stdlibContext = eval (StdlibLoader.load stdlib) emptyContext

[<Fact>]
let ``true should be a function returning the first of two arguments`` () =
    let expectedStack = Stack (Operation "foo", Empty)
    let _, actualStack = (eval "foo bar true app" stdlibContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``false should be a function returning the second of two arguments`` () =
    let expectedStack = Stack (Operation "bar", Empty)
    let _, actualStack = (eval "foo bar false app" stdlibContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``not should make true behave as false`` () =
    let expectedStack = Stack (Operation "bar", Empty)
    let _, actualStack = (eval "foo bar true not app" stdlibContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``not should make false behave as true`` () =
    let expectedStack = Stack (Operation "foo", Empty)
    let _, actualStack = (eval "foo bar false not app" stdlibContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``true should be equivalent (<=>) with true`` () =
    let expectedStack = Stack (Operation "foo", Empty)
    let _, actualStack = (eval "foo bar true true <=> app" stdlibContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``false should be equivalent (<=>) with false`` () =
    let expectedStack = Stack (Operation "foo", Empty)
    let _, actualStack = (eval "foo bar false false <=> app" stdlibContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``? (if/else) should apply the first of two quotations if given true`` () =
    let expectedStack = Stack (Operation "foo", Empty)
    let _, actualStack = (eval "true [foo] [bar] ?" stdlibContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``? (if/else) should apply the second of two quotations if given false`` () =
    let expectedStack = Stack (Operation "bar", Empty)
    let _, actualStack = (eval "false [foo] [bar] ?" stdlibContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``not should work with ? (if/else`` () =
    let expectedStack = Stack (Operation "foo", Empty)
    let _, actualStack = (eval "false not [foo] [bar] ?" stdlibContext)
    
    actualStack |> should equal expectedStack