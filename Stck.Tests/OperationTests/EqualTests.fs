module OperationTest.EqualTests

open Xunit
open FsUnit.Xunit
open Stck

let emptyContext = (Heap Map.empty, Empty)

[<Fact>]
let ``eq should return false if the two topmost elements on the stack are not equal`` () =
    let expectedStack = Stack (Quotation (Stack (Operation "false", Empty)), Empty)
    let _, actualStack = (eval "not equal eq" emptyContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``eq should return true if the two topmost elements on the stack are equal`` () =
    let expectedStack = Stack (Quotation (Stack (Operation "true", Empty)), Empty)
    let _, actualStack = (eval "[is [equal]] [is [equal]] eq" emptyContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``eq should raise a stack underflow exception if there are less than two elements on the stack`` () =
    let expectedStack = Stack (Exception StackUnderflow, Stack (Operation "one", Empty))
    let _, actualStack = (eval "one eq" emptyContext)
    
    actualStack |> should equal expectedStack