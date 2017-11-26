module OperationTest.ApplicationTests

open Xunit
open FsUnit.Xunit
open Stck

let emptyContext = (Heap Map.empty, Empty)

[<Fact>]
let ``Anonymous stacks should be applicable`` () =
    let expectedStack = Stack (Operation "last", Stack (Operation "next", Stack (Operation "first", Empty)))
    let _, actualStack = (eval "first [next last] app" emptyContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``Applying a word should raise a MissingQuotation exception`` () =
    let expectedStack = Stack (Exception MissingQuotation, Stack (Operation "a-word", Empty))
    let _, actualStack = (eval "a-word app" emptyContext)
    
    actualStack |> should equal expectedStack