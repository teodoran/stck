module OperationTest.DupTests

open Expecto
open Stck

let emptyContext = (Heap Map.empty, Empty)

[<Tests>]
let tests =
    testList "Dup tests" [
        testCase "dup should duplicate the topmost element on the stack" <| fun _ ->
            let expectedStack = Stack (Operation "duplicated", Stack (Operation "duplicated", Empty))
            let _, actualStack = (eval "duplicated dup" emptyContext)
            
            Expect.equal actualStack expectedStack "duplicated should be on the stack twice"

        testCase "dup should raise a stack underflow exception if the stack is empty" <| fun _ ->
            let expectedStack = Stack (Exception StackUnderflow, Empty)
            let _, actualStack = (eval "dup" emptyContext)
            
            Expect.equal actualStack expectedStack "A StackUnderflow exception should be on the stack"
    ]