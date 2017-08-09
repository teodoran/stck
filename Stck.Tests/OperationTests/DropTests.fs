module OperationTest.DropTests

open Expecto
open Stck

let emptyContext = (Heap Map.empty, Empty)

[<Tests>]
let tests =
    testList "Drop tests" [
        testCase "drop should remove an element from the stack" <| fun _ ->
            let context = (Heap Map.empty, Stack (Operation "this-should-be-removed", Empty))
            let expectedStack = Empty
            let _, actualStack = (eval "." context)
            
            Expect.equal actualStack expectedStack
                "this-should-be-removed from the stack"

        testCase "drop should raise a stack underflow exception if the stack is empty" <| fun _ ->
            let expectedStack = Stack (Exception StackUnderflow, Empty)
            let _, actualStack = (eval "." emptyContext)
            
            Expect.equal actualStack expectedStack
                "A StackUnderflow exception should be on the stack"

        testCase "drop should work in combination with push" <| fun _ ->
            let expectedStack = Stack (Operation "keep-this", Empty)
            let _, actualStack = (eval "keep-this drop-this and-drop-this . . drop-this ." emptyContext)
            
            Expect.equal actualStack expectedStack
                "keep-this was not the only item on the stack"

        testCase ".. should be interpreted as . ." <| fun _ ->
            let context = (Heap Map.empty, Stack (Operation "this", Stack (Operation "another", Empty)))
            let expectedStack = Empty
            let _, actualStack = (eval ".." context)
            
            Expect.equal actualStack expectedStack
                "The stack should be empty"
    ]