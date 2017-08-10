module OperationTest.EqualTests

open Expecto
open Stck

let emptyContext = (Heap Map.empty, Empty)

[<Tests>]
let tests =
    testList "Equal tests" [
        testCase "eq should return false if the two topmost elements on the stack are not equal" <| fun _ ->
            let expectedStack = Stack (Quotation (Stack (Operation "false", Empty)), Empty)
            let _, actualStack = (eval "not equal eq" emptyContext)
            
            Expect.equal actualStack expectedStack "false should be on the stack"

        testCase "eq should return true if the two topmost elements on the stack are equal" <| fun _ ->
            let expectedStack = Stack (Quotation (Stack (Operation "true", Empty)), Empty)
            let _, actualStack = (eval "[is [equal]] [is [equal]] eq" emptyContext)
            
            Expect.equal actualStack expectedStack "true should be on the stack"

        testCase "eq should raise a stack underflow exception if there are less than two elements on the stack" <| fun _ ->
            let expectedStack = Stack (Exception StackUnderflow, Stack (Operation "one", Empty))
            let _, actualStack = (eval "one eq" emptyContext)
            
            Expect.equal actualStack expectedStack "A StackUnderflow exception should be on the stack"
    ]