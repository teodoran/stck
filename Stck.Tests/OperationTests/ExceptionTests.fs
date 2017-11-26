module OperationTest.ExceptionTests

open Xunit
open FsUnit.Xunit
open Stck

let emptyContext = (Heap Map.empty, Empty)

[<Fact>]
let ``throw should push a user defined error to the stack`` () =
    let expectedStack = Stack (Exception (Failure "fail"), Empty)
    let _, actualStack = (eval "fail throw" emptyContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``throw should raise a stack underflow exception if the stack is empty`` () =
    let expectedStack = Stack (Exception StackUnderflow, Empty)
    let _, actualStack = (eval "throw" emptyContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``err should return true if the topmost element on the stack is an excaption`` () =
    let expectedStack = Stack (Quotation (Stack (Operation "true", Empty)), Empty)
    let _, actualStack = (eval "fail throw err" emptyContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``err should return false if the topmost element on the stack is not an excaption`` () =
    let expectedStack = Stack (Quotation (Stack (Operation "false", Empty)), Empty)
    let _, actualStack = (eval "not-exception err" emptyContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``err should raise a stack underflow exception if the stack is empty`` () =
    let expectedStack = Stack (Exception StackUnderflow, Empty)
    let _, actualStack = (eval "err" emptyContext)
    
    actualStack |> should equal expectedStack