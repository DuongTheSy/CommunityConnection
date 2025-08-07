from pydantic import BaseModel
from typing import List

class Goal(BaseModel):
    id: int
    user_id: int
    goal_name: str

class SuggestionRequest(BaseModel):
    current_user_id: int
    all_goals: List[Goal]
