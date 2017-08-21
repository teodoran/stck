module OperationTest.ChopTests

open Expecto
open Stck

let emptyContext = (Heap Map.empty, Empty)

[<Tests>]
let tests =
    testList "| (chop) tests" [
        testCase "| should take a quotations and return the first element and the rest of the quotation as two quotations" <| fun _ ->
            let expectedStack = Stack (Quotation (Stack (Operation "a", Empty)), Stack (Quotation (Stack (Operation "b", Stack (Operation "c", Empty))), Empty))
            let _, actualStack = (eval "[a b c] |" emptyContext)
            
            Expect.equal actualStack expectedStack "first and last should be in a quotation on the stack"

        testCase "| should handle empty quotations" <| fun _ ->
            let expectedStack = Stack (Quotation Empty, Stack (Quotation Empty, Empty))
            let _, actualStack = (eval "[] |" emptyContext)
            
            Expect.equal actualStack expectedStack "first and last should be in a quotation on the stack"

        testCase "| should raise a stack underflow exception if there are not a quotations on the stack" <| fun _ ->
            let expectedStack = Stack (Exception StackUnderflow, Stack (Operation "one", Empty))
            let _, actualStack = (eval "one |" emptyContext)
            
            Expect.equal actualStack expectedStack "A StackUnderflow exception should be on the stack"
    ]