module OperationTest.OntopTests

open Expecto
open Stck

let emptyContext = (Heap Map.empty, Empty)

[<Tests>]
let tests =
    testList "<< (ontop) tests" [
        testCase "<< should take an element and a quotation and push the element to the first position on the quotation" <| fun _ ->
            let expectedStack = Stack (Quotation (Stack (Operation "first", Stack (Operation "last", Empty))), Empty)
            let _, actualStack = (eval "[last] first <<" emptyContext)
            
            Expect.equal actualStack expectedStack "first and last should be in a quotation on the stack the stack"

        testCase "<< should raise a stack underflow exception if there are less than two elements on the stack" <| fun _ ->
            let expectedStack = Stack (Exception StackUnderflow, Stack (Operation "one", Empty))
            let _, actualStack = (eval "one <<" emptyContext)
            
            Expect.equal actualStack expectedStack "A StackUnderflow exception should be on the stack"
    ]