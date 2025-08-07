from fastapi import FastAPI
from models import SuggestionRequest, Goal
from typing import List
from sentence_transformers import SentenceTransformer, util
import torch

app = FastAPI()
model = SentenceTransformer('paraphrase-MiniLM-L6-v2')

@app.post("/suggest-users")
def suggest_users(data: SuggestionRequest):
    current_goals = [g for g in data.all_goals if g.user_id == data.current_user_id]
    other_goals = [g for g in data.all_goals if g.user_id != data.current_user_id]

    if not current_goals:
        return {"suggested_users": []}

    current_embeddings = model.encode(
        [g.goal_name for g in current_goals], convert_to_tensor=True
    )

    user_scores = {}
    for goal in other_goals:
        emb = model.encode(goal.goal_name, convert_to_tensor=True)
        score = float(util.pytorch_cos_sim(current_embeddings, emb).max().item())
        user_scores[goal.user_id] = max(user_scores.get(goal.user_id, 0), score)

    suggested_users = sorted(user_scores.items(), key=lambda x: x[1], reverse=True)

    return {
        "suggested_users": [
            {"user_id": uid, "score": round(score, 2)} for uid, score in suggested_users if score > 0.4
        ]
    }

@app.post("/suggest-mentor")
def suggest_users(data: SuggestionRequest):
    current_goals = [g for g in data.all_goals if g.user_id == data.current_user_id]
    other_goals = [g for g in data.all_goals if g.user_id != data.current_user_id]

    if not current_goals:
        return {"suggested_users": []}

    current_embeddings = model.encode(
        [g.goal_name for g in current_goals], convert_to_tensor=True
    )

    user_scores = {}
    for goal in other_goals:
        emb = model.encode(goal.goal_name, convert_to_tensor=True)
        score = float(util.pytorch_cos_sim(current_embeddings, emb).max().item())
        user_scores[goal.user_id] = max(user_scores.get(goal.user_id, 0), score)

    suggested_users = sorted(user_scores.items(), key=lambda x: x[1], reverse=True)

    return {
        "suggested_users": [
            {"user_id": uid, "score": round(score, 2)} for uid, score in suggested_users if score > 0.4
        ]
    }

