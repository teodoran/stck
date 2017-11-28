The STCK 2.0 Standard Library
=============================

Loading the standard library:
#load Stck/stdlib.stck

Derived stack operators
-----------------------
rot -> ```[[] swap << swap << swap >> app] rot #```

over -> ```[swap dup rot rot] over #```

2dup -> ```[over over] 2dup #```

Booleans
--------
Booleans uses the classic lambda calculus encoding

True = λa . λb . a
False = λa . λb . b

true -> ```[[.]] true #```
false -> ```[[swap .]] false #```

From there we can define boolean operators

IF = λp . λt . λe . p t e

if -> ```[rot app app] ? #```

not -> ```[[false] [true] ?] not #```

and -> ```[[[true] [false] ?] swap << [false] ?] and #```

or -> ```[not swap not and not] or #```

Implication and equivalence

```[not or] <= #```

```[swap <=] => #```

```[2dup => rot rot <= and] <=> #```

Math operations
---------------

Successor = (n) -> (f) -> (x) -> f(n(f)(x))

Man ønsker å konstruere noe som lager [huh] [noe] -> (succ_resultat) app -> [huh] [noe] app [huh] app
succ -> ```[| [swap dup rot swap ||] rot || ||] succ #```

### Some numbers
0 -> ```[[[.] app]] 0 #```
1 -> ```[0 succ] 1 #```
2 -> ```[1 succ] 2 #```
3 -> ```[2 succ] 3 #```

Man ønsker å konstruere noe som lager [huh] 2 3 * -> (2*3) app -> [[huh] 2 app] 3 app
multiplication -> ```[[swap rot swap [app] swap << swap << swap app] swap << swap <<] * #```

Man ønsker å konstruere noe som lager [huh] 2 3 + -> (2+3) app -> [huh] 2 app [huh] 3 app
addition -> ```[[app] swap << swap [app] swap << [rot dup rot swap << rot rot << || app] swap << swap <<] + #```

### Note!

Enkod tall som par, og velg det andre når du skal ha et tall som er en mindre.


Still not organized operators
-----------------------------

equal -> ```[eq app] = #```

error -> ```[err app] error #```

empty -> ```[dup error] empty #```

clear -> ```[empty [] [. clear] ?] clear #```