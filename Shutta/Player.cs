﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    public class Player
    {
        public Player(int money)
        {
            Money = money;

            _cards = new List<Card>();
        }

        public int Money { get; set; }

        private readonly List<Card> _cards;

        public void AddCard(Card card)
        {
            _cards.Add(card);
        }

        public Card GetCard(int index)
        {
            return _cards[index];
        }

        public void RemoveCard(int index)
        {
            _cards.RemoveAt(index);
        }

        public void SwitchCard(Player player)
        {
            Card tmp;

            tmp = _cards[1];
            _cards[1] = player._cards[1];
            player._cards[1] = tmp;
        }

        public virtual void CalculateScore()
        {
            if (_cards[0].No == _cards[1].No)
                Score = _cards[0].No * 10; // 10 ~ 100
            else
                Score = (_cards[0].No + _cards[1].No) % 10; // 0 ~ 9
        }

        public virtual int CalculateScore(int cardIndex1, int cardIndex2)
        {
            if (_cards[cardIndex1].No == _cards[cardIndex2].No)
                return _cards[cardIndex1].No * 10; // 10 ~ 100
            else
                return (_cards[cardIndex1].No + _cards[cardIndex2].No) % 10; // 0 ~ 9
        }

        public int Score { get; set; }

        // indexer
        public Card this[int index]
        {
            get
            {
                return _cards[index];
            }
        }

        public void PrepareRound()
        {
            _cards.Clear();
            Score = 0;
        }
    }
}
