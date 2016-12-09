#include "MyDialogBox.h"

USING_NS_CC;
USING_NS_PROJECT;

DialogBoxButton::DialogBoxButton(char* text, TextHAlignment align, ccMenuCallback callback)
{
	this->text = text;
	this->align = align;
	this->callback = callback;
}

//
Layer* MyDialogBox::create(std::vector<cocos2d::Label*> message)
{
	auto layer = MyDialogBox::create();
	layer->setParam(message);

	return layer;
}

Layer* MyDialogBox::create(std::vector<cocos2d::Label*> message, std::vector<DialogBoxButton> actionButtons)
{
	auto layer = MyDialogBox::create();
	layer->setParam(message, actionButtons);

	return layer;
}

Layer* MyDialogBox::create(char* message)
{
	auto layer = MyDialogBox::create();
	layer->setParam(message);

	return layer;
}

Layer* MyDialogBox::create(char* message, std::vector<DialogBoxButton> actionButtons)
{
	auto layer = MyDialogBox::create();
	layer->setParam(message, actionButtons);

	return layer;
}

void MyDialogBox::setParam(std::vector<cocos2d::Label*>& message)
{
	this->_message = message;
}

void MyDialogBox::setParam(std::vector<cocos2d::Label*>& message, std::vector<DialogBoxButton>& actionButtons)
{
	this->_message = message;
	this->_actionButtons = actionButtons;
}
void MyDialogBox::setParam(char* message)
{
	this->_message.push_back(Label::create(message, _fontName, _customFontSize));
}

void MyDialogBox::setParam(char* message, std::vector<DialogBoxButton>& actionButtons)
{
	this->_message.push_back(Label::create(message, _fontName, _customFontSize));
	this->_actionButtons = actionButtons;
}

void MyDialogBox::onEnter()
{
	Layer::onEnter();

	Size visibleSize = Director::getInstance()->getVisibleSize();

	Node* mainNode = Node::create();
	mainNode->setPosition(Point(visibleSize.width / 2, visibleSize.height / 2));
	this->addChild(mainNode);

	// draw background
	Sprite* backgroundSprite = Sprite::create("background1.png", Rect(Vec2::ZERO, Size(visibleSize.width, visibleSize.height)));
	backgroundSprite->setOpacity(200);
	mainNode->addChild(backgroundSprite);

	// draw 'Dialog Box' area
	Sprite* dialogSprite = Sprite::create("background1.png", Rect(Vec2::ZERO, Size(_dialogSize.width, _dialogSize.height)));
	mainNode->addChild(dialogSprite);

	// set postion ('Node') of the 'Text Area' (display message)
	Node* textArea = Node::create();
	textArea->setPosition(0, _dialogSize.height / 2 - (_dialogSize.height - _buttonsBarHeight - 2 * _dialogBoxBorderSize) / 2 - _dialogBoxBorderSize);
	mainNode->addChild(textArea);

	// draw background for the 'Text Area' (display message)
	Sprite* textAreaSprite = Sprite::create("black.png", Rect(Vec2::ZERO, Size(_dialogSize.width - 2 * _dialogBoxBorderSize, _dialogSize.height - _buttonsBarHeight - 2 * _dialogBoxBorderSize)));
	textArea->addChild(textAreaSprite);

	// display message of the 'Text Area' (from 'Labels' defined in '_message' array)
	Node* messageNode = Node::create();
	messageNode->addChild((_message.size() > 0) ? _message[0] : Label::create());
	for (int i = 1; i < _message.size(); i++)
	{
		_message[i]->setPosition(Vec2(0, _message[i-1]->getPosition().y - _message[i-1]->getFontDefinition()._fontSize/2 - _message[i]->getFontDefinition()._fontSize/2));
		messageNode->addChild(_message[i]);
	}

	messageNode->setPosition(0, (_message.size() > 1) ? ((-_message[_message.size()-1]->getPosition().y - _message[0]->getFontDefinition()._fontSize/2 + _message[_message.size()-1]->getFontDefinition()._fontSize/2) / 2) : 0);
	textArea->addChild(messageNode);

	// display interraction buttons ('Menu Item Labels' based on data defined in '_actionButtons' array)
	Vector<MenuItem*> menuItems;
	for (int i = 0; i < this->_actionButtons.size(); i++)
		menuItems.pushBack(createMenuItemLabel(this->_actionButtons[i]));

	Menu* menu = Menu::createWithArray(menuItems);
	menu->setPosition(Point(0, -(_dialogSize.height - (_dialogBoxBorderSize + textAreaSprite->getContentSize().height))));// -dialogSize.height / 2 + label->getContentSize().height / 2 + border));
	mainNode->addChild(menu);
}

bool MyDialogBox::init()
{
	if (!Layer::init())
	{
		return false;
	}

	// set size of the 'Dialog Box' from parameteres defined in 'Definitions.h' file
	_dialogSize = Size(DIALOG_BOX_WIDTH, DIALOG_BOX_HEIGHT);

	// custom 'Button' when there aren't any 'DialogBoxButton' defined
	_actionButtons.push_back(DialogBoxButton("OK", TextHAlignment::CENTER, CC_CALLBACK_1(MyDialogBox::close, this)));

	return true;
}

void MyDialogBox::close(Ref* sender)
{
	this->removeFromParent();
}

cocos2d::MenuItemLabel* MyDialogBox::createMenuItemLabel(DialogBoxButton button)
{
	Label* label = Label::create(button.text, _fontName, _manuItemLabelFontSize, Size(0, 0), button.align, cocos2d::TextVAlignment::CENTER);
	label->setColor(Color3B::BLACK);

	MenuItemLabel* playItem = MenuItemLabel::create(label, button.callback);
	
	Vec2 position;
	switch (button.align) 
	{
		case TextHAlignment::LEFT:
			position = Vec2(-_dialogSize.width / 2 + label->getContentSize().width / 2 + _dialogBoxBorderSize, 0);
			break;
		case TextHAlignment::RIGHT:
			position = Vec2(_dialogSize.width / 2 - label->getContentSize().width / 2 - _dialogBoxBorderSize, 0);
			break;
		case TextHAlignment::CENTER:
			position = Vec2::ZERO;
			break;
		default:
			position = Vec2::ZERO;
			break;
	}

	playItem->setPosition(position);

	return playItem;
}