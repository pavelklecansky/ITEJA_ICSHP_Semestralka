System.print("FizzBuzz Game")
var i = 1
while (i <= 100) {
    if (i % 3 == 0) {
	if (i % 5 == 0) {
            System.print("FizzBuzz")
    	}else{
	    System.print("Fizz")
	}
    } else {
	if (i % 5 == 0) {
            System.print("Buzz")
    	}else{
	    System.print(i)
	}
    }
    i = i + 1
}