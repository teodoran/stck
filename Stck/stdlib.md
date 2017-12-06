The STCK 2.0 Standard Library
=============================

Stck.Console handles loading of external STCK-code. You could e.g. load the standard library by using:

    load Stck/stdlib.stck

Derived stack operators
-----------------------
__rot__ -> ```[[] swap << swap << swap >> app] rot #```

__over__ -> ```[swap dup rot rot] over #```

__2dup__ -> ```[over over] 2dup #```

Booleans
--------
Booleans uses the classic lambda calculus encoding.

    true = λa . λb . a / false = λa . λb . b

__true__ -> ```[[.]] true #```

__false__ -> ```[[swap .]] false #```

From there we can define boolean operators.

    if = λp . λt . λe . p t e

__if__ -> ```[rot app app] ? #```
__not__ -> ```[[false] [true] ?] not #```
__and__ -> ```[[[true] [false] ?] swap << [false] ?] and #```
__or__ -> ```[not swap not and not] or #```

We can compare booleans with implication and equivalence.

__Left implication__ -> ```[not or] <- #```

__Right implication__ -> ```[swap <-] -> #```

__Equivalence__ -> ```[2dup -> rot rot <- and] <-> #```

Math operations
---------------

Numerals is also encoded using Church encoding. We start with the successor function.

    successor = (n) -> (f) -> (x) -> f(n(f)(x))

__succ__ -> ```[| [swap dup rot swap ||] rot || ||] succ #```

Then we can define some numbers.

0. ```[[[.] app]] 0 #```
1. ```[0 succ] 1 #```
2. ```[1 succ] 2 #```
3. ```[2 succ] 3 #```
4. ```[3 succ] 4 #```
5. ```[4 succ] 5 #```
6. ```[5 succ] 6 #```
7. ```[6 succ] 7 #```
8. ```[7 succ] 8 #```
9. ```[8 succ] 9 #```
10. ```[9 succ] 10 #```
100. ```[10 10 *] 100 #```
1000. ```[100 10 *] 1000 #```
1000000. ```[1000 1000 *] 1M #```

We can multiply and add the numbers together.

__multiplication__ -> ```[[swap rot swap [app] swap << swap << swap app] swap << swap <<] * #```

__addition__ -> ```[[app] swap << swap [app] swap << [rot dup rot swap << rot rot << || app] swap << swap <<] + #```

Then for the tricky part, defining the predecessor function. The general ide is to group together a number and boolean.

__pred-first__ -> ```[0 false] pred-first #```

Then you want a function that increments the number so it's one less than the number of times the function has been called. It goes something like this:

0. `pred-first` -> `0 false`
1. `pred-first pred-next` -> `0 true`
2. `pred-first pred-next pred-next` -> `1 true`
3. ...

__pred-next__ -> ```[[succ true] [true] ?] pred-next #```

Now we can construct the predecessor function by applying the number to pred-first and pred-next, and then drop the boolean at the end.

__pred__ -> ```[pred-first rot [pred-next] swap app .] pred #```

With a predecessor function we can define subtraction.

__subtraction__ -> ```[[pred] swap app] - #```

We might also want some predicates to convert numbers to booleans.

__is-zero__ -> ```[true [. false] rot app] is-zero #```

__less-or-equal__ -> ```[swap - is-zero] <= #```

__greater-or-equal__ -> ```[- is-zero] >= #```

__equal__ -> ```[2dup >= rot rot <= and] = #```

Finally we can make a remainder/modulo operation.

__remainder__ -> ```[2dup <= [dup rot swap - swap %] [.] ?] % #```


Operators currently in beta
---------------------------
You should probably not use these operators before they're out of beta...

__error__ -> ```[err app] error #```

__empty__ -> ```[emp app] empty #```

__clear__ -> ```[empty [] [. clear] ?] clear #```
