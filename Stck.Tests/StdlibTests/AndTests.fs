module StdlibTests.AndTests

open Expecto
open Stck

let emptyContext = (Heap Map.empty, Empty)
let stdlibContext = eval (StdlibLoader.load stdlib) emptyContext

[<Tests>]
let tests =
    testList "and tests" [
        testCase "true true and should be true #1" <| fun _ ->
            let expectedStack = Stack (Operation "foo", Empty)
            let _, actualStack = (eval "true true and [foo] [bar] ?" stdlibContext)
            
            Expect.equal actualStack expectedStack "true true and [foo] [bar] ? should return foo"

        testCase "true true and should be true #2" <| fun _ ->
            let expectedStack = Stack (Operation "foo", Empty)
            let _, actualStack = (eval "foo bar true true and app" stdlibContext)
            
            Expect.equal actualStack expectedStack "foo bar true true and app should return foo"

        testCase "false true and should be false #1" <| fun _ ->
            let expectedStack = Stack (Operation "bar", Empty)
            let _, actualStack = (eval "false true and [foo] [bar] ?" stdlibContext)
            
            Expect.equal actualStack expectedStack "false true and [foo] [bar] ? should return bar"

        testCase "false true and should be false #2" <| fun _ ->
            let expectedStack = Stack (Operation "bar", Empty)
            let _, actualStack = (eval "foo bar false true and app" stdlibContext)
            
            Expect.equal actualStack expectedStack "foo bar false true and app should return bar"

        testCase "true false and should be false #1" <| fun _ ->
            let expectedStack = Stack (Operation "bar", Empty)
            let _, actualStack = (eval "true false and [foo] [bar] ?" stdlibContext)
            
            Expect.equal actualStack expectedStack "true false and [foo] [bar] ? should return bar"

        testCase "true false and should be false #2" <| fun _ ->
            let expectedStack = Stack (Operation "bar", Empty)
            let _, actualStack = (eval "foo bar true false and app" stdlibContext)
            
            Expect.equal actualStack expectedStack "foo bar true false and app should return bar"

        testCase "false false and should be false #1" <| fun _ ->
            let expectedStack = Stack (Operation "bar", Empty)
            let _, actualStack = (eval "false false and [foo] [bar] ?" stdlibContext)
            
            Expect.equal actualStack expectedStack "false false and [foo] [bar] ? should return bar"

        testCase "false false and should be false #2" <| fun _ ->
            let expectedStack = Stack (Operation "bar", Empty)
            let _, actualStack = (eval "foo bar false false and app" stdlibContext)
            
            Expect.equal actualStack expectedStack "foo bar false false and app should return bar"
    ]