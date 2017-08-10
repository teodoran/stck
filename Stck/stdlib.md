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

TRUE = λa . λb . a

FALSE = λa . λb . b

true -> ```[[app .]] true #```

false -> ```[[app swap .]] false #```

From there we can define boolean operators

IF = λp . λt . λe . p t e

if -> ```[[] swap << swap << swap app app] ? #```

not -> ```[[false] [true] ?] not #```

and -> ```[[[true] [false] ?] swap << [false] ?] and #```

or -> ```[not swap not and not] or #```

Implication and equivalence

```[not or] <- #```

```[swap <-] -> #```

```[2dup -> rot rot <- and] <-> #```

Math operations
---------------

onemore -> ```[[] swap <<] onemore #```

addition -> ```[2dup 0 = [..] [. swap onemore swap app +] ?] + #```

### Some numbers
0 -> [0] 0 #

1 -> ```[0 onemore] 1 #```

2 -> ```[0 onemore onemore] 2 #```

3 -> ```[0 onemore onemore onemore] 3 #```


Still not organized operators
-----------------------------

equal -> ```[eq app] = #```

error -> ```[err app] error #```

empty -> ```[dup error] empty #```

clear -> ```[empty [] [. clear] ?] clear #```