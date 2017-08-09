module OperationTest.PushTests

open Expecto
open Stck

let emptyContext = (Heap Map.empty, Empty)

[<Tests>]
let tests =
    testList "Push tests" [
        testCase "An empty program should not modify the context" <| fun _ ->
            Expect.equal (eval "" emptyContext) emptyContext
                "Empty program and context should give empty context"

        testCase "A symbol should be pushed to the stack" <| fun _ ->
            let expectedStack = Stack (Operation "my-symbol", Empty)
            let _, actualStack = (eval "my-symbol" emptyContext)
            
            Expect.equal actualStack expectedStack
                "my-symbol should be on the stack"

        testCase "Several symbols should be pushed to the stack" <| fun _ ->
            let expectedStack = Stack (Operation "last", Stack (Operation "next", Stack (Operation "first", Empty)))
            let _, actualStack = (eval "first next last" emptyContext)
            
            Expect.equal actualStack expectedStack
                "first, next and last should be on the stack"
    ]