let currentIndex = 0;

$(document).ready(function () {
    loadQuestion();

    function loadQuestion() {
        $.get("/QuizApp/GetQuestion", { index: currentIndex }, function (data) {
            if (data.end) {
                window.location.href = "/QuizApp/Result";
            } else {
                $("#quiz-container").html(data);
            }
        });
    }

    $(document).on("submit", "#question-form", function (e) {
        e.preventDefault();
        const selectedOption = $("input[name='option']:checked").val();
        const questionId = $("#questionId").val();

        if (!selectedOption) {
            alert("Please select an option.");
            return;
        }

        $.post("/QuizApp/SubmitAnswer", {
            questionId: questionId,
            selectedQuestionId: selectedOption
        }, function () {
            currentIndex++;
            loadQuestion();
        });
    });
});
