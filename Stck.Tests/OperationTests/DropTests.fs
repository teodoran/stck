module OperationTest.DropTests

open Xunit
open FsUnit.Xunit
open Stck

let emptyContext = (Heap Map.empty, Empty)

[<Fact>]
let ``drop should remove an element from the stack`` () =
    let context = (Heap Map.empty, Stack (Operation "this-should-be-removed", Empty))
    let expectedStack = Empty
    let _, actualStack = (eval "." context)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``drop should raise a stack underflow exception if the stack is empty`` () =
    let expectedStack = Stack (Exception StackUnderflow, Empty)
    let _, actualStack = (eval "." emptyContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``drop should work in combination with push`` () =
    let expectedStack = Stack (Operation "keep-this", Empty)
    let _, actualStack = (eval "keep-this drop-this and-drop-this . . drop-this ." emptyContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``.. should be interpreted as . .`` () =
    let context = (Heap Map.empty, Stack (Operation "this", Stack (Operation "another", Empty)))
    let expectedStack = Empty
    let _, actualStack = (eval ".." context)
    
    actualStack |> should equal expectedStack