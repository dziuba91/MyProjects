#ifndef __MYDIALOG_LAYER_H__
#define __MYDIALOG_LAYER_H__

#include "cocos2d.h"
#include "Definitions.h"

PROJECT_NS_BEGIN

/**
* standard 'buttons' structure for the project
* define for 'MyDialogBox' class, of current project
*/
struct DialogBoxButton
{
	DialogBoxButton(char*, cocos2d::TextHAlignment, cocos2d::ccMenuCallback);

	char* text;
	cocos2d::TextHAlignment align;
	cocos2d::ccMenuCallback callback;
};

/**
* define 'Layer' represents custom 'Dialog Box' for the current project
* allow a simple interaction with the game
* content of the 'Dialog Box' can be modify by the custom constructors of the class
*/
class MyDialogBox : public cocos2d::Layer
{
public:
	static cocos2d::Layer* create(std::vector<cocos2d::Label*> message);
	static cocos2d::Layer* create(std::vector<cocos2d::Label*> message, std::vector<DialogBoxButton> actionButtons);
	static cocos2d::Layer* create(char* message);
	static cocos2d::Layer* create(char* message, std::vector<DialogBoxButton> actionButtons);
	
	CREATE_FUNC(MyDialogBox);

private:
	void setParam(std::vector<cocos2d::Label*>& message);
	void setParam(std::vector<cocos2d::Label*>& message, std::vector<DialogBoxButton>& actionButtons);
	void setParam(char* message);
	void setParam(char* message, std::vector<DialogBoxButton>& actionButtons);

	virtual bool init();
	virtual void onEnter();

	/** create new 'MenuItemLabel' besed on the data from 'DialogBoxButton' object */
	cocos2d::MenuItemLabel* createMenuItemLabel(DialogBoxButton button);

	/** close the Dialog Box */
	void close(cocos2d::Ref* sender);

	//
	std::vector<cocos2d::Label*> _message; // * list of Labels to display in the Text Area
	std::vector<DialogBoxButton> _actionButtons; // * list of interaction buttons

	cocos2d::Size _dialogSize; // * size of the dialog box (defined in 'Definitions.h')

	// @Properties:
	const int _minLabelLineSpacing = 0; // * minimal space beetwen two labels in the Text Area ('message box')
	const int _dialogBoxBorderSize = 20; // * border of the 'dialog box'
	const int _manuItemLabelFontSize = 30; // * font size of 'buttons' ('Menu Item Labels') in Dialog Box
	const int _customFontSize = 30; // * font size of a 'message' in the 'Text Area'
	const int _buttonsBarHeight = 80; // * height of the area dedicated to 'buttons'
	const char* _fontName = "Calibri"; // * font used in the all 'dialog box' Labels
};

PROJECT_NS_END

#endif // __MYDIALOG_LAYER_H__