module OperationTest.DupTests

open Xunit
open FsUnit.Xunit
open Stck

let emptyContext = (Heap Map.empty, Empty)

[<Fact>]
let ``dup should duplicate the topmost element on the stack`` () =
    let expectedStack = Stack (Operation "duplicated", Stack (Operation "duplicated", Empty))
    let _, actualStack = (eval "duplicated dup" emptyContext)
    
    actualStack |> should equal expectedStack   

[<Fact>]
let ``dup should raise a stack underflow exception if the stack is empty`` () =
    let expectedStack = Stack (Exception StackUnderflow, Empty)
    let _, actualStack = (eval "dup" emptyContext)
    
    actualStack |> should equal expectedStack   