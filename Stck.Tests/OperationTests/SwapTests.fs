module OperationTest.SwapTests

open Expecto
open Stck

let emptyContext = (Heap Map.empty, Empty)

[<Tests>]
let tests =
    testList "Swap tests" [
        testCase "swap should swap the two topmost elements on the stack" <| fun _ ->
            let expectedStack = Stack (Operation "last", Stack (Operation "first", Empty))
            let _, actualStack = (eval "last first swap" emptyContext)
            
            Expect.equal actualStack expectedStack "first and last should be on the stack"

        testCase "swap should raise a stack underflow exception if there are less than two elements on the stack" <| fun _ ->
            let expectedStack = Stack (Exception StackUnderflow, Stack (Operation "one", Empty))
            let _, actualStack = (eval "one swap" emptyContext)
            
            Expect.equal actualStack expectedStack "A StackUnderflow exception should be on the stack"
    ]