OBJ = main.o screen.o telesim.o
APPNAME = teleApp.a
INCLUDE = screen.h telesim.h

$(APPNAME) : $(OBJ)
	gcc -o $(APPNAME) $(OBJ)

%.o : %.c $(INCLUDE)
	gcc -c -std=c99 -o $@ $<

clean :
	rm $(OBJ) $(APPNAME)
