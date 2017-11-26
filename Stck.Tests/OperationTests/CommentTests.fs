module OperationTest.CommentTests

open Xunit
open FsUnit.Xunit
open Stck

let emptyContext = (Heap Map.empty, Empty)

[<Fact>]
let ``A comment should be ignored`` () =
    let _, actualStack = (eval "```This is a comment```" emptyContext)
    
    actualStack |> should equal Empty

[<Fact>]
let ``Comments should not affect other parts of the program`` () =
    let expectedStack = Stack (Operation "last", Stack (Operation "first", Empty))
    let _, actualStack = (eval "first ```should not be affected``` last" emptyContext)
    
    actualStack |> should equal expectedStack