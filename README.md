STCK
====
_a stack-based programming language_

_"INTERCAL allows only 2 different types of variables, the 16-bit integer and the 32-bit integer." - From the [INTERCAL Programming Language Revised Reference Manual](http://www.muppetlabs.com/~breadbox/intercal-man/s03.html)_

STCK (pronounced stick) is is a programming languague inspired by [Forth](https://en.wikipedia.org/wiki/Forth_(programming_language)). Variables are never declared, values is just placed on a global stack. Syntax is minimal. The only supported data-type is 32-bit integers.


Installation
------------

You'll need a F# compiler (fsharpc/fshapri) to compile the STCK interpreter. [This guide](http://fsharp.org/use/linux/) is usefull if you're using Linux. When the compiler is installed, navigate to the folder containing `stck.fs` and run:

    fsharpc stck.fs && ./stck.exe ./samples/euler-one.stck

This should compile the interpreter into stck.exe, and run a stck-program, solving the first [Project Euler](https://projecteuler.net/) problem. Just running stck.exe will launch the interactive interpreter.


Using the languague
-------------------

_"Beware of the Turing tar-pit in which everything is possible but nothing of interest is easy." - [Alan J. Perlis](http://pu.inf.uni-tuebingen.de/users/klaeren/epigrams.html)_

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

_"A goal of a good Forth programmer is to minimize the use of these words, since they are only moving data around instead of doing useful work." - From the [The OLPC Wiki](http://wiki.laptop.org/go/Forth_stack_operators)_

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

`2dup` will duplicate the two topmost elements on the stack.

    2 3
    [3; 2]
    2dup
    [3; 2; 3; 2]

`over` will copy the second element on the stack.

    2 3
    [3; 2]
    over
    [2; 3; 2]

`rot` will move the third element to the top of the stack.

    1 2 3
    [3; 2; 1]
    rot
    [1; 3; 2]

`len` will return the size of the stack and push the size onto the stack.

    1 1
    [1; 1]
    len
    [2; 1; 1]

`empty` will check if the stack is empty, and push the result onto the stack.

    []
    empty
    [1]

`max` and `min` will remove all elements on the stack, leaving only the largest or smallest integer.

    1 3 1 max
    [3]
    1 -2 1 min
    [-2]

In addition, the [Forth dokumentation](http://wiki.laptop.org/go/Forth_stack_operators) has a good description of different stack operators, along with reference implementations for less basic operators.

**Math**

_"For every epsilon>0 there is a delta>0 such that whenever |x-x_0|<delta, then |f(x)-f(x_0)|<epsilon." - From [Wikipedia](https://en.wikipedia.org/wiki/(%CE%B5,_%CE%B4)-definition_of_limit)_

The following operators are supported: `+` (addition), `-` (substraction), `*` (multiplication), `/` (division) `i/` (integer division) and `%` (modulo). All operators, except `/` perform on the two upmost elements on the stack, and push the result back on the stack as one number.

    5 2 -
    [3]

`/` divides two integers, and push the result of the division onto the stack as two numbers (integer quotient and remainder). The remainder is always given as a number with six digits. In the case where the remainder exceeds six digits, no rounding is performed.

    2 3 /
    [666666; 0]

In addition, the remainder can be computed directly with the `rem` operator.

    2 3 rem
    [666666]

**Boolean operators**

False is represented by `0`(zero) and anything else is considered true. The following boolean operators are supported: `=` (equal), `>` (greater than), `<` (less than), and `not`. All operators, except `not`, perform on the two upmost elements on the stack, and push the result back on the stack.

**Conditionals**

Conditionals follow the if-then-else construct: `<a boolean> ? <this will happen if true> : <this will happen if false> ;`

    3 3 = ? 1337 : 192 ;
    [1337]

    3 17 = ? 1337 : 192 ;
    [192]

**Subroutines**

_"What is a definition? Well classically a definition was colon something, and words, and end of definition somewhere." - [Chuck Moore](http://www.ultratechnology.com/1xforth.htm)_

Subroutines are declared by using `#`:

    # add-five 5 +
    []
    2 add-five
    [7]

The contents of a subroutine is contained within a line. So remeber to terminate the subroutine declaration with `!` when not using the interactive interpreter.

    // some-file.stck !
    
    # add-five 5 + !
    
    2 add-five !

**Comments**

_"Due to INTERCAL's implementation of comment lines, most error messages are produced during execution instead of during compilation." - From the [INTERCAL Programming Language Revised Reference Manual](http://www.muppetlabs.com/~breadbox/intercal-man/s09.html)_

`//` indicates the start of a comment. Comments are considered as statements, and therefore has to be delimited as regulare lines with `!`.

    // This is a comment !
    
    // This 
    is also 
    a commet !

**Utility functions**

`hprint` prints the content of the heap. This will list all declared subroutines.
`sprint` prints the content of the stack. This is equal to the reply given by the interpreter.
`quit` exits the interprenter.
