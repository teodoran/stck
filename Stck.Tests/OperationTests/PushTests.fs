module OperationTest.PushTests

open Xunit
open FsUnit.Xunit
open Stck

let emptyContext = (Heap Map.empty, Empty)

[<Fact>]
let ``An empty program should not modify the context`` () =
    (eval "" emptyContext) |> should equal emptyContext


[<Fact>]
let ``A symbol should be pushed to the stack`` () =
    let expectedStack = Stack (Operation "my-symbol", Empty)
    let _, actualStack = (eval "my-symbol" emptyContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``Several symbols should be pushed to the stack`` () =
    let expectedStack = Stack (Operation "last", Stack (Operation "next", Stack (Operation "first", Empty)))
    let _, actualStack = (eval "first next last" emptyContext)
    
    actualStack |> should equal expectedStack