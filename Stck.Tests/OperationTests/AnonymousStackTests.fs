module OperationTest.AnonymousStackTests

open Expecto
open Stck

let emptyContext = (Heap Map.empty, Empty)

[<Tests>]
let tests =
    testList "Anonymous stack (quotation) tests" [
        testCase "An anonymous stack should be pushed to the stack" <| fun _ ->
            let expectedStack = Stack (Quotation (Stack (Operation "anonymous", Stack (Operation "stack", Empty))), Empty)
            let _, actualStack = (eval "[anonymous stack]" emptyContext)
            
            Expect.equal actualStack expectedStack
                "A quotation should be on the stack"
        
        testCase "Nested anonymous stacks should be pushed to the stack" <| fun _ ->
            let expectedStack = Stack (Quotation (Stack (Operation "anonymous", Stack (Quotation (Stack (Operation "nested", Empty)), Stack (Operation "stack", Empty)))), Empty)
            let _, actualStack = (eval "[anonymous [nested] stack]" emptyContext)
            
            Expect.equal actualStack expectedStack
                "A quotation with a nested quotation should be on the stack"
    ]