#include <iostream>
#include <string>
#include <deque>

using namespace std;

class huge_number
{
	deque<uint8_t> _number; // * coding format: 4 bits for 1 digit (BCD)
	bool _isNegative = false; // * sign of the number (+, -)
	
	enum COMPARE_RESULT : int { GREATER, LOWER, EQUAL };
	enum BCD : int { HIGHER_NIBBLE, LOWER_NIBBLE };
	
public:
	huge_number();
	huge_number(deque<uint8_t>);
	huge_number(deque<uint8_t>, bool);
	
	huge_number& operator= (const string&);
	
	huge_number operator+ (const huge_number&);
	
	huge_number operator- (const huge_number&);
	
	huge_number operator* (const huge_number&);
	
	huge_number operator/ (const huge_number&);
	
	string toString();

private:
	/**
	* pack 2 digits to 1 byte variable
	*
	* @param higherDigit: get first digit (to 'packed number' in 0xf0) 
	* @param lowerDigit: get second digit (to 'packed number' in 0x0f)
	* @return: packed number
	*/
	uint8_t pack_bcd(uint8_t higherDigit, uint8_t lowerDigit);
	
	/**
	* unpack 1 digit from 1 byte variable
	*
	* @param value: get packed variable
	* @param nibble: select digit to return (from higher, or lower nibble) - should use 'BCD' enum type
	*/
	uint8_t unpack_bcd(uint8_t value, int nibble);
	
	uint8_t char_to_digit(char);
	char digit_to_char(uint8_t);
	
	void overflow_control(int8_t& value, uint8_t& carryBit);
	
	/**
	* shift left the parameter by 4 bits (1 digit)
	*/
	void shift_left(deque<uint8_t>&);
	
	/**
	* add new digit to back side of the number (index = number size)
	* e.g.: number=3242335 and digit=4 -> result=32423354
	*/
	void push_digit(deque<uint8_t>&, uint8_t);
	
	deque<uint8_t> add(const deque<uint8_t>&, const deque<uint8_t>&);
	deque<uint8_t> del(const deque<uint8_t>&, const deque<uint8_t>&);
	
	/** 
	* multiply numbers in format: number x digit
	* e.g.: 541 x 1, 3 x 7, 423423534543234 x 4
	*/
	deque<uint8_t> mul(const deque<uint8_t>& number, uint8_t digit);
	
	/** 
	* multiply numbers in format: number x digit (with string of 0)
	* e.g.: 541 x 10, 3 x 700, 423423534543234 x 4000000 
	*/
	deque<uint8_t> mul(const deque<uint8_t>& number, uint8_t digit, unsigned int numberOfZeros);
	
	/** 
	* standard division of 2 'huge_numbers'
	* should be use only if result is 1 digit
	*/
	uint8_t div(const deque<uint8_t>&, const deque<uint8_t>&);
	
	int compare(const deque<uint8_t>&);
	int compare(const deque<uint8_t>&, const deque<uint8_t>&);
};

ostream& operator<< (ostream&, huge_number);

//
// MAIN start
main ()
{
	unsigned int Z, index;
	string tmp;
	huge_number A, B;

	cin >> Z;

	for (; Z>0; Z--)
	{
		cin >> tmp;
		
		index = tmp.find_first_of("+-/*");
		
		A = tmp.substr(0, index);
		B = tmp.substr(index+1);
		
		switch (tmp[index])
		{
			case '+':
				cout << A+B << endl;
			break;
			
			case '*':
				cout << A*B << endl;
			break;
			
			case '-':
				cout << A-B << endl;
			break;
			
			case '/':
				cout << A/B << endl;
			break;
			
			default: break;
		}
	}
	
	return EXIT_SUCCESS;
}
// MAIN end

//
huge_number::huge_number() { }

huge_number::huge_number(deque<uint8_t> number)
{
	this->_number = number;
}

huge_number::huge_number(deque<uint8_t> number, bool isNegative)
{
	this->_number = number;
	this->_isNegative = isNegative;
}

huge_number& huge_number::operator= (const string& val) 
{
	this->_number.clear();
	
	unsigned int i=0;
	
	if (val.length()%2)
		this->_number.push_back(char_to_digit(val[i++]));
			
	for (; i<val.length(); i+=2)
		this->_number.push_back(pack_bcd(char_to_digit(val[i]), char_to_digit(val[i+1])));
	
	return *this;
}

huge_number huge_number::operator+ (const huge_number& val) 
{	
	return add(this->_number, val._number);
}

huge_number huge_number::operator- (const huge_number& val) 
{	
	switch(compare(val._number))
	{
		case COMPARE_RESULT::LOWER:
		return huge_number(del(val._number, this->_number), true);
		
		case COMPARE_RESULT::EQUAL:
		return huge_number( {0} );
		
		default: break; // continue if defaoult, so: COMPARE_RESULT::GREATER
	}
	
	return del(this->_number, val._number); // case: COMPARE_RESULT::GREATER
}

huge_number huge_number::operator* (const huge_number& val) 
{
	deque<uint8_t> result = {0};
	
	if (((val._number.size() == 1) && (val._number[0] == 0)) || (val._number.size() == 0)) { }
	else
	{
		for (unsigned int i=0; i<val._number.size(); i++)
		{
			result = add(result, mul(this->_number, unpack_bcd(val._number[i], BCD::HIGHER_NIBBLE), 2*(val._number.size()-i)-1));
			
			result = add(result, mul(this->_number, unpack_bcd(val._number[i], BCD::LOWER_NIBBLE), 2*(val._number.size()-i)-2));
		}
	}
	
	return result;
}

huge_number huge_number::operator/ (const huge_number& val) 
{
	deque<uint8_t> result = {0};
	
	if (((val._number.size() == 1) && (val._number[0] == 0)) || (val._number.size() == 0)) { } // the operation should be forbidden
	else
	{
		switch (compare(val._number))
		{
			case COMPARE_RESULT::LOWER:
			return result;
			
			case COMPARE_RESULT::EQUAL:
				result[0] = 1;
			return result;
			
			default: break; // continue if default, so: COMPARE_RESULT::GREATER
		}
		
		// case: COMPARE_RESULT::GREATER
		uint8_t tmp;
		deque<uint8_t> dividend = {0};
		const BCD bcdNibbleArray[] = { HIGHER_NIBBLE, LOWER_NIBBLE };
		
		for (unsigned int i=0; i<this->_number.size(); i++)
			for (BCD nibble : bcdNibbleArray)
			{
				push_digit(dividend, unpack_bcd(this->_number[i], nibble));
				
				switch (compare(dividend, val._number))
				{
					case COMPARE_RESULT::GREATER:
					case COMPARE_RESULT::EQUAL:
						tmp = div(dividend, val._number);
						push_digit(result, tmp);
						dividend = del(dividend, mul(val._number, tmp));
					break;
				
					case COMPARE_RESULT::LOWER:
						shift_left(result);
					break;
					
					default: break;
				}
			}
	}
	
	return result;
}

string huge_number::toString()
{
	string txt = (this->_isNegative ? "-" : "");
	
	uint8_t tmp;
	for (unsigned int i=0; i<this->_number.size(); i++)
	{
		tmp = unpack_bcd(this->_number[i], BCD::HIGHER_NIBBLE);
		if ((i == 0) && (tmp == 0))
		{
			txt += digit_to_char(unpack_bcd(this->_number[0], BCD::LOWER_NIBBLE));
			continue;
		}
			
		txt += digit_to_char(tmp);
		txt += digit_to_char(unpack_bcd(this->_number[i], BCD::LOWER_NIBBLE));
	}
	
	return txt;
}

//
deque<uint8_t> huge_number::add(const deque<uint8_t>& A, const deque<uint8_t>& B)
{
	deque<uint8_t> result;

	int8_t lowerNibble, higherNibble;
	uint8_t carry = 0;
	for (int i=A.size()-1, j=B.size()-1; ; i--, j--)
	{
		if ((j >= 0) && (i >= 0))
		{
			lowerNibble = unpack_bcd(A[i], BCD::LOWER_NIBBLE) + unpack_bcd(B[j], BCD::LOWER_NIBBLE) + carry;
			overflow_control(lowerNibble, carry);
			
			higherNibble = unpack_bcd(A[i], BCD::HIGHER_NIBBLE) + unpack_bcd(B[j], BCD::HIGHER_NIBBLE) + carry;
			overflow_control(higherNibble, carry);
		}
		else if (i >= 0)
		{
			lowerNibble = unpack_bcd(A[i], BCD::LOWER_NIBBLE) + carry;
			overflow_control(lowerNibble, carry);
			
			higherNibble = unpack_bcd(A[i], BCD::HIGHER_NIBBLE) + carry;
			overflow_control(higherNibble, carry);
		}
		else if (j >= 0)
		{
			lowerNibble = unpack_bcd(B[j], BCD::LOWER_NIBBLE) + carry;
			overflow_control(lowerNibble, carry);
			
			higherNibble = unpack_bcd(B[j], BCD::HIGHER_NIBBLE) + carry;
			overflow_control(higherNibble, carry);
		}
		else if (carry)
		{
			lowerNibble = 1;
			higherNibble = 0;
			
			carry = 0;
		}
		else break;
		
		result.push_front(pack_bcd(higherNibble, lowerNibble));
	}
	
	return result;
}

deque<uint8_t> huge_number::del(const deque<uint8_t>& A, const deque<uint8_t>& B)
{
	deque<uint8_t> result;
	
	int8_t lowerNibble, higherNibble;
	uint8_t carry = 0;
	for (int i=A.size()-1, j=B.size()-1; ; i--, j--)
	{
		if ((j >= 0) && (i >= 0))
		{
			lowerNibble = unpack_bcd(A[i], BCD::LOWER_NIBBLE) - unpack_bcd(B[j], BCD::LOWER_NIBBLE) - carry;
			overflow_control(lowerNibble, carry);
			
			higherNibble = unpack_bcd(A[i], BCD::HIGHER_NIBBLE) - unpack_bcd(B[j], BCD::HIGHER_NIBBLE) - carry;
			overflow_control(higherNibble, carry);
		}
		else if (i >= 0)
		{
			lowerNibble = unpack_bcd(A[i], BCD::LOWER_NIBBLE) - carry;
			overflow_control(lowerNibble, carry);
			
			higherNibble = unpack_bcd(A[i], BCD::HIGHER_NIBBLE) - carry;
			overflow_control(higherNibble, carry);
		}
		else break;
		
		result.push_front(pack_bcd(higherNibble, lowerNibble));
	}
	
	// reduce 'result' size if need: delete useless 0 from the front of the number (e.g. 000322, to: 0322)
	while ((result[0]==0) && (result.size()>1))
		result.pop_front();
	
	return result;
}

deque<uint8_t> huge_number::mul(const deque<uint8_t>& number, uint8_t digit)
{
	deque<uint8_t> result = {0};
	
	if (digit != 0)
		for (int i=0; i<digit; i++)
		{
			result = this->add(result, number);
		}
	
	return result;
}
	
deque<uint8_t> huge_number::mul(const deque<uint8_t>& number, uint8_t digit, unsigned int numberOfZeros)
{
	deque<uint8_t> result = {0};
	
	if (digit != 0)
	{
		result = this->mul(number, digit);
		
		if (numberOfZeros != 0)
		{
			if (numberOfZeros%2 == 1)
				shift_left(result);
			
			for (unsigned int i = 0; i<(numberOfZeros/2); i++)
				result.push_back(0);
		}
	}
	
	return result;
}

uint8_t huge_number::div(const deque<uint8_t>& A, const deque<uint8_t>& B)
{
	deque<uint8_t> tmp = A;
	uint8_t count=0;
	
	while(compare(tmp, B) != COMPARE_RESULT::LOWER)
	{
		tmp = del(tmp, B);
		
		count++;
	}
	
	return count;
}

//	
uint8_t huge_number::pack_bcd(uint8_t higherDigit, uint8_t lowerDigit)
{
	return (((higherDigit&0x0f)<<4) | (lowerDigit&0x0f));
}

uint8_t huge_number::unpack_bcd(uint8_t value, int nibble)
{
	return ((nibble == BCD::HIGHER_NIBBLE) ? ((value>>4)&0x0f) : (value&0x0f));
}

void huge_number::shift_left(deque<uint8_t>& number)
{
	if ((number.size()==0) || ((number.size()==1) && (number[0]==0))) { }
	else
	{
		unsigned int i = 0;
	
		uint8_t tmp = unpack_bcd(number[i], BCD::HIGHER_NIBBLE);
		if (tmp != 0)
		{
			number.push_front(tmp);
			i++;
		}
	
		for (; i < number.size(); i++)
		{
			tmp = (unpack_bcd(number[i], BCD::LOWER_NIBBLE)<<4)&0xf0;
				
			if (i+1 < number.size())
				tmp |= (unpack_bcd(number[i+1], BCD::HIGHER_NIBBLE)&0x0f);
				
			number[i] = tmp;
		}
	}
}

void huge_number::push_digit(deque<uint8_t>& number, uint8_t digit)
{
	shift_left(number);
	
	number[number.size()-1] |= (digit&0x0f);
}
	
uint8_t huge_number::char_to_digit(char c)
{
	return c - 48;
}
	
char huge_number::digit_to_char(uint8_t n)
{
	return n + 48;
}

void huge_number::overflow_control(int8_t& value, uint8_t& carryBit)
{
	if (value >= 10)
	{
		carryBit = 1;
		value -= 10;
	}
	else if (value < 0)
	{
		carryBit = 1;
		value += 10;
	}
	else carryBit = 0;
}

//
int huge_number::compare(const deque<uint8_t>& A, const deque<uint8_t>& B)
{
	if(A.size() > B.size())
		return COMPARE_RESULT::GREATER;
	else if (A.size() < B.size())
		return COMPARE_RESULT::LOWER;
	else
	{
		uint8_t digitA, digitB;
		const BCD bcdNibbleArray[] = { HIGHER_NIBBLE, LOWER_NIBBLE };
		
		for (unsigned int i=0; i<A.size(); i++)
			for (BCD nibble : bcdNibbleArray)
			{
				digitA = unpack_bcd(A[i], nibble);
				digitB = unpack_bcd(B[i], nibble);
				if (digitA > digitB)
					return COMPARE_RESULT::GREATER;
				else if (digitA < digitB)
					return COMPARE_RESULT::LOWER;
			}
	}
	
	return COMPARE_RESULT::EQUAL;
}

int huge_number::compare(const deque<uint8_t>& number)
{
	return this->compare(this->_number, number);
}

ostream& operator<< (ostream& out, huge_number number)
{
	return out << number.toString();
}
