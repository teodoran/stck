module OperationTest.SwapTests

open Xunit
open FsUnit.Xunit
open Stck

let emptyContext = (Heap Map.empty, Empty)

[<Fact>]
let ``swap should swap the two topmost elements on the stack`` () =
    let expectedStack = Stack (Operation "last", Stack (Operation "first", Empty))
    let _, actualStack = (eval "last first swap" emptyContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``swap should raise a stack underflow exception if there are less than two elements on the stack`` () =
    let expectedStack = Stack (Exception StackUnderflow, Stack (Operation "one", Empty))
    let _, actualStack = (eval "one swap" emptyContext)
    
    actualStack |> should equal expectedStack