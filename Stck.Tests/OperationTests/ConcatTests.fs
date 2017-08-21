module OperationTest.ConcatTests

open Expecto
open Stck

let emptyContext = (Heap Map.empty, Empty)

[<Tests>]
let tests =
    testList "|| (concat) tests" [
        testCase "|| should take two quotations and concat the contests of the second onto the first" <| fun _ ->
            let expectedStack = Stack (Quotation (Stack (Operation "a", Stack (Operation "b", Stack (Operation "c", Stack (Operation "d", Empty))))), Empty)
            let _, actualStack = (eval "[a b] [c d] ||" emptyContext)
            
            Expect.equal actualStack expectedStack "first and last should be in a quotation on the stack"

        testCase "|| should raise a stack underflow exception if there are not two quotations on the stack" <| fun _ ->
            let expectedStack = Stack (Exception StackUnderflow, Stack (Quotation (Stack (Operation "one", Empty)), Empty))
            let _, actualStack = (eval "[one] ||" emptyContext)
            
            Expect.equal actualStack expectedStack "A StackUnderflow exception should be on the stack"
    ]