module OperationTest.AnonymousStackTests

open Xunit
open FsUnit.Xunit
open Stck

let emptyContext = (Heap Map.empty, Empty)

[<Fact>]
let ``An anonymous stack should be pushed to the stack`` () =
    let expectedStack = Stack (Quotation (Stack (Operation "anonymous", Stack (Operation "stack", Empty))), Empty)
    let _, actualStack = (eval "[anonymous stack]" emptyContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``Nested anonymous stacks should be pushed to the stack`` () =
    let expectedStack = Stack (Quotation (Stack (Operation "anonymous", Stack (Quotation (Stack (Operation "nested", Empty)), Stack (Operation "stack", Empty)))), Empty)
    let _, actualStack = (eval "[anonymous [nested] stack]" emptyContext)
    
    actualStack |> should equal expectedStack