stck
====
_a stack-based programming language_

stck is is a programming languague inspired by [Forth](https://en.wikipedia.org/wiki/Forth_(programming_language)). Variables are never declared, values is just placed on a global stack. Syntax is minimal. Currently, the only supported data-type is 32-bit integers.

Installation
------------

You'll need a F# compiler (fsharpc/fshapri) to compile the stck interpreter. [This guide](http://fsharp.org/use/linux/) is usefull if you're using Linux. When the compiler is installed, simply run:

    fsharpc stck.fs && ./stck.exe ./euler-one.stck

This should compile the interpreter into stck.exe, and run a stck-program, solving the first [Project Euler](https://projecteuler.net/) problem. Just running stck.exe will launch the interactive interpreter.

Using the languague
-------------------
**Delimiting lines**

When using the interpreter, return acts as the line delimiter.
    2 3 +
    [5]
The entire statement must fit on one line, as multi-line statements is not supported.

When executing files, `!` acts as the line-delimiter, making it possible to declare statements over several lines.
    2 3
    +
    !
Lines cannot be nested.

**working with the stack**

`.` will drop one element from the stack.

    2 3 4
    [4; 3; 2]
    .
    [3; 2]

`swap` will swap the two upmost elements on the stack.

    2 3 4
    [3; 2; 4]
    swap
    [2; 3; 4]

`dup` will duplicate the topmost element on the stack (TOS)

    2
    [2]
    dup
    [2; 2]
    
**Math**

The following operators are supported: `+` (addition), `-` (substraction), `*` (multiplication), `/` (division) and `%` (modulo). All operators perform on the two upmost elements on the stack, and push the result back on the stack. Beware of integer division as it will floor the result.

    5 2 -
    [3]

**Boolean operators**

False is represented by `0`(zero) and anything else is considered true. The following boolean operators are supported: `=` (equal), `>` (greater than), `<` (less than), and `not`. All operators perform on the two upmost elements on the stack, and push the result back on the stack.

**Conditionals**

Conditionals follow the if-then-else construct: `<a boolean> ? <this will happen if true> : <this will happen if false> ;`

    3 3 = ? 1337 : 192 ;
    [1337]

    3 17 = ? 1337 : 192 ;
    [192]

**Comments**

`//` indicates the start of a comment. Comments are considered as statements, and therefore has to be delimited as regulare lines with `!`.

    // This is a comment !
    
    // This 
    is also 
    a commet !

**Utility functions**

`hprint` prints the content of the heap. This will list all declared subroutines.
`sprint` prints the content of the stack. This is equal to the reply given by the interpreter.


