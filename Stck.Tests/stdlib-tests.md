Stdlib Tests
============
_This file contains all the tests for the STCK standard library. It's mostly Church encoding._

booleans and boolean operations
-------------------------------

### false and true
```
[foo bar true app] [foo] eq 
[foo bar false app] [bar] eq 
```

### not
```
[true not] [false] eq 
[false not] [true] eq 
```

### equivalence (<->)
```
[true true <->] [true] eq 
[false false <->] [true] eq 
[true false <-> not] [true] eq 
[false true <-> not] [true] eq 
```

### conditionals (?)
```
[true [foo] [bar] ?] [foo] eq 
[false [foo] [bar] ?] [bar] eq 
[false not [foo] [bar] ?] [foo] eq 
```

### and
```
[true true and] [true] eq 
[false false and not] [true] eq 
[false true and not] [true] eq 
[true false and not] [true] eq 
```

### or
```
[true true or] [true] eq 
[false true or] [true] eq 
[true false or] [true] eq 
[false false or not] [true] eq 
```

numerals
--------

### zero (0)
```[x [f] 0 app] [x] eq```

### successor function (succ)
```
[0 succ 1 =] [true] eq 
[0 succ succ 2 =] [true] eq 
[3 succ 4 =] [true] eq 
```

### numerals
```
[x [f] 1 app] [x f] eq 
[x [f] 2 app] [x f f] eq 
```

### multiplication (*)
```
[2 3 * 6 =] [true] eq 
[1 2 * 2 =] [true] eq 
[0 3 * 0 =] [true] eq 
[3 0 * 0 =] [true] eq 
```

### addition (+)
```
[1 1 + 2 =] [true] eq 
[2 3 + 5 =] [true] eq 
[1 2 + 3 =] [true] eq 
[0 3 + 3 =] [true] eq 
[3 0 + 3 =] [true] eq 
```

### pred-first
```
[x [f] pred-first . app] [x] eq 
[x [f] pred-first app app] [] eq 
```

### pred-next
```
[x [f] pred-first pred-next app] [x [f]] eq 
[x [f] pred-first pred-next . app] [x] eq 
[x [f] pred-first pred-next pred-next . app] [x f] eq 
[x [f] pred-first pred-next pred-next pred-next . app] [x f f] eq 
[x [f] pred-first [pred-next] 3 app . app] [x f f] eq 
[x [f] pred-first [pred-next] 3 2 * app . app] [x f f f f f] eq 
```

### predecessor
```
[3 pred 2 =] [true] eq 
[3 2 * pred 5 =] [true] eq 
[2 2 * pred 3 + 6 =] [true] eq 
```

### is-zero
```
[0 is-zero] [true] eq 
[1 is-zero not] [true] eq 
[3 is-zero not] [true] eq 
```

### substraction (-)
```
[3 2 - 1 =] [true] eq 
[2 0 - 2 =] [true] eq 
[1 3 - 0 =] [true] eq 
```

### less or equal (<=)
```
[2 3 <= not] [true] eq 
[3 0 <=] [true] eq 
[1 1 <=] [true] eq 
```

### greater or equal (>=)
```
[0 2 >=] [true] eq 
[3 2 >= not] [true] eq 
[1 1 >=] [true] eq 
```

### equal (=)
```
[2 2 =] [true] eq 
[3 2 = not] [true] eq 
```

### remainder/modulo (%)
```
[2 2 % 0 =] [true] eq 
[6 2 % 0 =] [true] eq 
[5 3 % 2 =] [true] eq 
[3 5 % 3 =] [true] eq 
```
