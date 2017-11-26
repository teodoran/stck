module OperationTest.ChopTests

open Xunit
open FsUnit.Xunit
open Stck

let emptyContext = (Heap Map.empty, Empty)

[<Fact>]
let ``| should take a quotations and return the first element and the rest of the quotation as two quotations`` () =
    let expectedStack = Stack (Quotation (Stack (Operation "a", Empty)), Stack (Quotation (Stack (Operation "b", Stack (Operation "c", Empty))), Empty))
    let _, actualStack = (eval "[a b c] |" emptyContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``| should handle empty quotations`` () =
    let expectedStack = Stack (Quotation Empty, Stack (Quotation Empty, Empty))
    let _, actualStack = (eval "[] |" emptyContext)
    
    actualStack |> should equal expectedStack

[<Fact>]
let ``| should raise a stack underflow exception if there are not a quotations on the stack`` () =
    let expectedStack = Stack (Exception StackUnderflow, Stack (Operation "one", Empty))
    let _, actualStack = (eval "one |" emptyContext)
    
    actualStack |> should equal expectedStack