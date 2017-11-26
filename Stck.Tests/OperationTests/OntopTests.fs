module OperationTest.OntopTests

open Xunit
open FsUnit.Xunit
open Stck

let emptyContext = (Heap Map.empty, Empty)

[<Fact>]
let ``<< should take an element and a quotation and push the element to the first position on the quotation`` () =
    let expectedStack = Stack (Quotation (Stack (Operation "first", Stack (Operation "last", Empty))), Empty)
    let _, actualStack = (eval "[last] first <<" emptyContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``<< should raise a stack underflow exception if there are less than two elements on the stack`` () =
    let expectedStack = Stack (Exception StackUnderflow, Stack (Operation "one", Empty))
    let _, actualStack = (eval "one <<" emptyContext)
    
    actualStack |> should equal expectedStack