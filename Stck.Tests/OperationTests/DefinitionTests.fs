module OperationTest.DefinitionTests

open Expecto
open Stck

let emptyContext = (Heap Map.empty, Empty)

[<Tests>]
let tests =
    testList "Definition tests" [
        testCase "An definition should define a new word" <| fun _ ->
            let expectedHeap = Heap (Map.add "my-word" (Stack (Operation "word-content", Empty)) Map.empty)
            let actualHeap, _ = (exec "[word-content] my-word #" emptyContext)
            
            Expect.equal actualHeap expectedHeap "my-word should be defined on the heap"
        
        testCase "An definition should not modify the stack" <| fun _ ->
            let expectedHeap = Heap (Map.add "my-word" (Stack (Operation "content", Empty)) Map.empty)
            let _, actualStack = (exec "[word-content] my-word #" emptyContext)
            
            Expect.equal actualStack Empty "The stack should be empty"

        testCase "A defined word should be able to modify the stack" <| fun _ ->
            let expectedStack = Stack (Operation "word-content", Empty)
            let _, actualStack = (exec "[word-content] my-word # my-word" emptyContext)
            
            Expect.equal actualStack expectedStack "word-content should be on the stack"

        testCase "Defining a word should raise a StackUnderflow exception if the stack does not contain a symbol and a quotation" <| fun _ ->
            let expectedStack = Stack (Exception StackUnderflow, Stack (Operation "my-word", Stack (Operation "not-quotation", Empty)))
            let _, actualStack = (exec "not-quotation my-word #" emptyContext)
            
            Expect.equal actualStack expectedStack "word-content should be on the stack"
    ]