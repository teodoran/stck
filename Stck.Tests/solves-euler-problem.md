Solves Project Euler problem #2
===============================
This standalone STCK program exists in order to test code-file execution.

This file can be executed by running `dotnet run --project Stck.Console/Stck.Console.fsproj -- Stck.Tests/solves-euler-problem.md`, and should print `[10]` to the console when doing so.

Solving the problem
-------------------
_Considering the terms in the Fibonacci sequence whose values do not exceed 10, find the sum of the even-valued terms._

### Supporting functions
Before trying to solve the overall problem, we'll want some supporting functions.

Generates the next Fibonacci number if the two previous values are at the top of the stack (TOS).
```
[2dup +] next-fib #
```

Checks if a number on TOS is even.
```
[dup 2 % 0 =] is-even #
```

Checks if a number on TOS is zero. Used to determine if stack is close to empty.
```
[dup 0 =] next-is-zero #
```

Recursively generates all Fibonacci numbers under 10.
```
[
    next-fib dup 10 >=
        [fib-under-10]
        [.] ?
] fib-under-10 #
```

Recursively sums all even numbers on the stack. Will stopp summing numbers when it encounters a zero.
```
[
    swap is-even [+] [.] ?
    swap next-is-zero [.] [swap sum-if-even] ?
] sum-if-even #
```

### Solving the overall problem
Now we're ready to solve Euler problem #2 by:
1. Placing a zero on the stack, so we can determine when to stop summing numbers.
2. Placing the first two numbers in the Fibonacci sequence on the stack, so we can start generating Fibonacci numbers.
3. Generates all Fibonacci numbers below 10 by calling `fib-under-10`.
4. Sums all even Fibonacci numbers by calling `sum-if-even`.
```
0 1 2 fib-under-10 sum-if-even
```