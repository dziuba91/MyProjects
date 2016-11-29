#include "Draw.h"

#include <windows.h>
#include <gl/GL.h>

USING_NS_ENGINE;

void Draw::drawCube(Vector3D size, Vector3D pos, const GLfloat* color)
{
	glLoadIdentity();

	glTranslatef(pos.x, pos.y, pos.z);

	cube(size/2, color);
}

void Draw::drawCube(Vector3D size, Vector3D pos, const GLfloat* color, Vector3D rot)
{
	glLoadIdentity();

	glTranslatef(pos.x, pos.y, pos.z);

	glRotatef(rot.x, 1.f, 0.f, 0.f);
	glRotatef(rot.y, 0.f, 1.f, 0.f);
	glRotatef(rot.z, 0.f, 0.f, 1.f);
	
	cube(size/2, color);
}

void Draw::cube(Vector3D size, const GLfloat* color)
{
	glBegin(GL_QUADS);
		glMaterialfv(GL_FRONT_AND_BACK, GL_AMBIENT, color);
		glMaterialfv(GL_FRONT_AND_BACK, GL_DIFFUSE, color);
		glMaterialfv(GL_FRONT_AND_BACK, GL_SPECULAR, color);

		glNormal3f(0.0f, 0.0f, 1.0f);
		glVertex3f(-size.x, -size.y, size.z);
		glVertex3f(size.x, -size.y, size.z);
		glVertex3f(size.x, size.y, size.z);
		glVertex3f(-size.x, size.y, size.z);

		glNormal3f(0.0f, 0.0f, -1.0f);
		glVertex3f(-size.x, -size.y, -size.z);
		glVertex3f(-size.x, size.y, -size.z);
		glVertex3f(size.x, size.y, -size.z);
		glVertex3f(size.x, -size.y, -size.z);

		glNormal3f(0.0f, 1.0f, 0.0f);
		glVertex3f(-size.x, size.y, -size.z);
		glVertex3f(-size.x, size.y, size.z);
		glVertex3f(size.x, size.y, size.z);
		glVertex3f(size.x, size.y, -size.z);

		glNormal3f(0.0f, -1.0f, 0.0f);
		glVertex3f(-size.x, -size.y, -size.z);
		glVertex3f(size.x, -size.y, -size.z);
		glVertex3f(size.x, -size.y, size.z);
		glVertex3f(-size.x, -size.y, size.z);

		glNormal3f(1.0f, 0.0f, 0.0f);
		glVertex3f(size.x, -size.y, -size.z);
		glVertex3f(size.x, size.y, -size.z);
		glVertex3f(size.x, size.y, size.z);
		glVertex3f(size.x, -size.y, size.z);

		glNormal3f(-1.0f, 0.0f, 0.0f);
		glVertex3f(-size.x, -size.y, -size.z);
		glVertex3f(-size.x, -size.y, size.z);
		glVertex3f(-size.x, size.y, size.z);
		glVertex3f(-size.x, size.y, -size.z);
	glEnd();
}