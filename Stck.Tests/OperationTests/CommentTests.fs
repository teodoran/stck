module OperationTest.CommentTests

open Expecto
open Stck

let emptyContext = (Heap Map.empty, Empty)

[<Tests>]
let tests =
    testList "Comment tests" [
        testCase "A comment should be ignored" <| fun _ ->
            let _, actualStack = (exec "\"This is a comment\"" emptyContext)
            
            Expect.equal actualStack Empty
                "the stack should be empty"
        
        testCase "Comments should not affect other parts of the program" <| fun _ ->
            let expectedStack = Stack (Operation "last", Stack (Operation "first", Empty))
            let _, actualStack = (exec "first \"should not be affected\" last" emptyContext)
            
            Expect.equal actualStack expectedStack
                "first and last should be on the stack"
    ]