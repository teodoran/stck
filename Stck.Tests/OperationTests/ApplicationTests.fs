module OperationTest.ApplicationTests

open Expecto
open Stck

let emptyContext = (Heap Map.empty, Empty)

[<Tests>]
let tests =
    testList "Application tests" [
        testCase "Anonymous stacks should be applicable" <| fun _ ->
            let expectedStack = Stack (Operation "last", Stack (Operation "next", Stack (Operation "first", Empty)))
            let _, actualStack = (exec "first [next last] app" emptyContext)
            
            Expect.equal actualStack expectedStack
                "next and last should be applied to the stack, along with first"

        testCase "Applying a word should raise a MissingQuotation exception" <| fun _ ->
            let expectedStack = Stack (Exception MissingQuotation, Empty)
            let _, actualStack = (exec "a-word app" emptyContext)
            
            Expect.equal actualStack expectedStack
                "MissingQuotation exception should be on the stack"
    ]