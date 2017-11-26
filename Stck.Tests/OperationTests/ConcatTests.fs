module OperationTest.ConcatTests

open Xunit
open FsUnit.Xunit
open Stck

let emptyContext = (Heap Map.empty, Empty)

[<Fact>]
let ``|| should take two quotations and concat the contests of the second onto the first`` () =
    let expectedStack = Stack (Quotation (Stack (Operation "a", Stack (Operation "b", Stack (Operation "c", Stack (Operation "d", Empty))))), Empty)
    let _, actualStack = (eval "[a b] [c d] ||" emptyContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``|| should raise a stack underflow exception if there are not two quotations on the stack`` () =
    let expectedStack = Stack (Exception StackUnderflow, Stack (Quotation (Stack (Operation "one", Empty)), Empty))
    let _, actualStack = (eval "[one] ||" emptyContext)
    
    actualStack |> should equal expectedStack