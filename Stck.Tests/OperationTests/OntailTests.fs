module OperationTest.OntailTests

open Xunit
open FsUnit.Xunit
open Stck

let emptyContext = (Heap Map.empty, Empty)

[<Fact>]
let ``>> should take an element and a quotation and push the element to the last position on the quotation`` () =
    let expectedStack = Stack (Quotation (Stack (Operation "last", Stack (Operation "first", Empty))), Empty)
    let _, actualStack = (eval "[last] first >>" emptyContext)
    
    actualStack |> should equal expectedStack