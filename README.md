python -m venv venv
venv\Scripts\activate       # Windows

pip install -r requirements.txt
uvicorn main:app --reload
