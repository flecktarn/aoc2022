int score(int them, int you)
{
    int pts = 0;

    if (them == you)
    {
        pts = 3;
    }
    else if ((them + 1) % 3 == you)
    {
        pts = 6;
    }
    
    return you + pts;
}