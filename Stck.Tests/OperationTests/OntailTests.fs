module OperationTest.OntailTests

open Expecto
open Stck

let emptyContext = (Heap Map.empty, Empty)

[<Tests>]
let tests =
    testList ">> (ontail) tests" [
        testCase ">> should take an element and a quotation and push the element to the last position on the quotation" <| fun _ ->
            let expectedStack = Stack (Quotation (Stack (Operation "last", Stack (Operation "first", Empty))), Empty)
            let _, actualStack = (exec "[last] first >>" emptyContext)
            
            Expect.equal actualStack expectedStack "first and last should be in a quotation on the stack the stack"
    ]