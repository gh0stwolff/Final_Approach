using System;
using GXPEngine;

public struct Vec2 
{
	public float x;
	public float y;

	public Vec2 (float pX = 0, float pY = 0) 
	{
		x = pX;
		y = pY;
	}

	public static Vec2 operator+ (Vec2 left, Vec2 right) {
		return new Vec2(left.x+right.x, left.y+right.y);
	}

	public static Vec2 operator -(Vec2 left, Vec2 right)
	{
		return new Vec2(left.x - right.x, left.y - right.y);
	}

	public static Vec2 operator *(float left, Vec2 right)
	{
		return new Vec2(left * right.x, left * right.y);
	}

	public static Vec2 operator *(Vec2 left, float right)
	{
		return new Vec2(left.x * right, left.y * right);
	}

    public static Vec2 operator /(Vec2 left, float right)
    {
        return new Vec2(left.x / right, left.y / right);
    }

	public override string ToString () 
	{
		return String.Format ("({0}; {1})", x, y);
	}

    public float Length()
    {
        return (float)Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
    }

    public void Normalize()
    {
        float length = Length();
        if (length == 0) { return; }
        if (x != 0) { x *= (1 / length); }
        if (y != 0) { y *= (1 / length); }
    }

    public Vec2 Normalized()
    {
        float length = Length();
        if (length == 0) { return new Vec2(0, 0); }
        float X = x;
        float Y = y;

        if (X != 0) { X *= (1 / length); }
        if (Y != 0) { Y *= (1 / length); }

        return new Vec2(X, Y);
    }

    public void SetXY(float X, float Y)
    {
        x = X;
        y = Y;
    }

    public static Vec2 Lerp(Vec2 start, Vec2 end, float t)
    {
        t = Mathf.Clamp(t, 0, 1);
        return start + (end - start) * t;
    }

    public static float Deg2Rad(float degree)
    {
        return degree / 180 * Mathf.PI;
    }

    public static float Rad2Deg(float radian)
    {
        return radian * 180 / Mathf.PI;
    }

    public static Vec2 GetUnitVectorDeg(float degrees)
    {
        float radians = Deg2Rad(degrees);
        return GetUnitVectorRad(radians);
    }

    public static Vec2 GetUnitVectorRad(float radians)
    {
        Vec2 vectorDegree;
        vectorDegree.x = Mathf.Cos(radians);
        vectorDegree.y = Mathf.Sin(radians);
        return vectorDegree;
    }

    public static Vec2 RandomUnitVector()
    {
        return GetUnitVectorDeg(Utils.Random(0, 359));
    }

    public void SetAngleDegrees(float degrees)
    {
        float radians = Deg2Rad(degrees);
        SetAngleRadians(radians);
    }

    public void SetAngleRadians(float radians)
    {
        float length = Length();
        x = Mathf.Cos(radians) * length;
        y = Mathf.Sin(radians) * length;
    }

    public float GetAngleDegrees()
    {
        return Rad2Deg(GetAngleRadians());
    }

    public float GetAngleRadians()
    {
        return (float)Math.Atan2(y, x);
    }

    public void RotateDegrees(float degrees)
    {
        RotateRadians(Deg2Rad(degrees));
    }

    public void RotateRadians(float radians)
    {
        SetXY(x * Mathf.Cos(radians) - y * Mathf.Sin(radians), x * Mathf.Sin(radians) + y * Mathf.Cos(radians));
    }

    public void RotateAroundDegrees(Vec2 point, float degrees)
    {
        this -= point;
        RotateDegrees(degrees);
        this += point;
    }

    public void RotateAroundRadians(Vec2 point, float radians)
    {
        this -= point;
        RotateRadians(radians);
        this += point;
    }

    public static Vec2 PointOfImpact(Vec2 position, Vec2 velocity, Vec2 barrierPoint)
    {
        Vec2 delta = position - barrierPoint;

        if (delta.x > delta.y)
        {
            float deltaY = barrierPoint.y - (position.y + velocity.y);
            float deltaX = velocity.x * (deltaY / velocity.y);

            return new Vec2(deltaX + position.x + velocity.x, deltaY + position.y + velocity.y);
        }
        else
        {
            float deltaX = barrierPoint.x - (position.x + velocity.x);
            float deltaY = velocity.y * (deltaX / velocity.x);

            return new Vec2(deltaX + position.x + velocity.x, deltaY + position.y + velocity.y);
        }
    }

    public float Dot(Vec2 other)
    {
        float angleA = this.GetAngleRadians() - other.GetAngleRadians();
        return this.Length() * other.Length() * Mathf.Cos(angleA);
    }

    public Vec2 Normal()
    {
        Vec2 normal = GetUnitVectorRad(GetAngleRadians());
        normal.RotateDegrees(90);
        return normal;
    }
}