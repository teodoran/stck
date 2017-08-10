module OperationTest.ExceptionTests

open Expecto
open Stck

let emptyContext = (Heap Map.empty, Empty)

[<Tests>]
let tests =
    testList "Exception tests" [
        testCase "throw should push a user defined error to the stack" <| fun _ ->
            let expectedStack = Stack (Exception (Failure "fail"), Empty)
            let _, actualStack = (eval "fail throw" emptyContext)
            
            Expect.equal actualStack expectedStack "true should be on the stack"

        testCase "throw should raise a stack underflow exception if the stack is empty" <| fun _ ->
            let expectedStack = Stack (Exception StackUnderflow, Empty)
            let _, actualStack = (eval "throw" emptyContext)
            
            Expect.equal actualStack expectedStack "A StackUnderflow exception should be on the stack"
        
        testCase "err should return true if the topmost element on the stack is an excaption" <| fun _ ->
            let expectedStack = Stack (Quotation (Stack (Operation "true", Empty)), Empty)
            let _, actualStack = (eval "fail throw err" emptyContext)
            
            Expect.equal actualStack expectedStack "true should be on the stack"

        testCase "err should return false if the topmost element on the stack is not an excaption" <| fun _ ->
            let expectedStack = Stack (Quotation (Stack (Operation "false", Empty)), Empty)
            let _, actualStack = (eval "not-exception err" emptyContext)
            
            Expect.equal actualStack expectedStack "true should be on the stack"

        testCase "err should raise a stack underflow exception if the stack is empty" <| fun _ ->
            let expectedStack = Stack (Exception StackUnderflow, Empty)
            let _, actualStack = (eval "err" emptyContext)
            
            Expect.equal actualStack expectedStack "A StackUnderflow exception should be on the stack"
    ]