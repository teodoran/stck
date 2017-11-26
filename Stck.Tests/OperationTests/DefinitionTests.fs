module OperationTest.DefinitionTests

open Xunit
open FsUnit.Xunit
open Stck

let emptyContext = (Heap Map.empty, Empty)

[<Fact>]
let ``An definition should define a new word`` () =
    let expectedHeap = Heap (Map.add "my-word" (Stack (Operation "word-content", Empty)) Map.empty)
    let actualHeap, _ = (eval "[word-content] my-word #" emptyContext)
    
    actualHeap |> should equal expectedHeap

[<Fact>]
let ``An definition should not modify the stack`` () =
    let expectedHeap = Heap (Map.add "my-word" (Stack (Operation "content", Empty)) Map.empty)
    let _, actualStack = (eval "[word-content] my-word #" emptyContext)
    
    actualStack |> should equal Empty

[<Fact>]
let ``A defined word should be able to modify the stack`` () =
    let expectedStack = Stack (Operation "word-content", Empty)
    let _, actualStack = (eval "[word-content] my-word # my-word" emptyContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``Defining a word should raise a StackUnderflow exception if the stack does not contain a symbol and a quotation`` () =
    let expectedStack = Stack (Exception StackUnderflow, Stack (Operation "my-word", Stack (Operation "not-quotation", Empty)))
    let _, actualStack = (eval "not-quotation my-word #" emptyContext)
    
    actualStack |> should equal expectedStack