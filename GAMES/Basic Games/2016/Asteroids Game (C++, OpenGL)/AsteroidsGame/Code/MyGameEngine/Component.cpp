#include "Component.h"

USING_NS_ENGINE;

void Component::init() { }

void Component::draw() { }

void Component::update() { }

void Component::reset() { }

void Component::onExit() { }

void Component::setComponentID(unsigned int id)
{
	if (id) this->_ID = id;
}

bool Component::isComponentID(unsigned int id)
{
	return (this->_ID == id);
}